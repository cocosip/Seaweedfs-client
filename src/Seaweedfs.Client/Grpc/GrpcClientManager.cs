using MasterPb;
using Microsoft.Extensions.Logging;
using Seaweedfs.Client.Extensions;
using Seaweedfs.Client.Scheduling;
using Seaweedfs.Client.Util;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VolumeServerPb;
using GoogleGrpc = Grpc.Core;

namespace Seaweedfs.Client.Grpc
{
    /// <summary>Grpc客户端管理
    /// </summary>
    public class GrpcClientManager : IGrpcClientManager
    {
        private bool _isRunning = false;
        private readonly object SyncObject = new object();
        private readonly ILogger _logger;
        private readonly IScheduleService _scheduleService;
        private readonly SeaweedfsOption _option;
        private GoogleGrpc.Channel _masterLeaderChannel = null;

        /// <summary>Ctor
        /// </summary>
        public GrpcClientManager(ILoggerFactory loggerFactory, IScheduleService scheduleService, SeaweedfsOption option)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
            _scheduleService = scheduleService;
            _option = option;
        }


        /// <summary>获取Master Grpc客户端
        /// </summary>
        public Seaweed.SeaweedClient GetMasterClient()
        {
            if (_masterLeaderChannel == null)
            {
                _logger.LogError("MasterLeaderChannel为Null,无法创建客户端");
            }
            var client = new Seaweed.SeaweedClient(_masterLeaderChannel);
            return client;
        }



        /// <summary>运行
        /// </summary>
        public void Start()
        {
            if (_isRunning)
            {
                return;
            }
            _masterLeaderChannel = CreateMasterChannel();
            //开启同步
            StartGrpcSyncMasterLeaderTask();

            //var channel=new GoogleGrpc.Channel("127.0.0.1")

            _isRunning = true;
        }

        /// <summary>停止
        /// </summary>
        public void Shutdown()
        {
            StopGrpcSyncMasterLeaderTask();
            _isRunning = false;
        }


        /// <summary>创建默认的Master连接
        /// </summary>
        private GoogleGrpc.Channel CreateMasterChannel(ConnectionAddress exceptMaster = null)
        {
            var masterServers = _option.GrpcOption.GrpcMasters;
            if (exceptMaster != null)
            {
                masterServers = masterServers.Where(x => x.IPAddress != exceptMaster.IPAddress && x.Port != exceptMaster.Port).ToList();
            }
            var master = masterServers.FirstOrDefault();
            if (master == null)
            {
                throw new ArgumentException("配置文件中不包含任何Master节点的配置.");
            }

            var connectionAddress = new ConnectionAddress()
            {
                IPAddress = master.IPAddress,
                Port = master.Port
            };
            return CreateChannel(connectionAddress);
        }

        /// <summary>根据连接地址创建Channel
        /// </summary>
        private GoogleGrpc.Channel CreateChannel(ConnectionAddress connectionAddress)
        {
            GoogleGrpc.Channel channel = new GoogleGrpc.Channel(connectionAddress.IPAddress, connectionAddress.Port, GoogleGrpc.ChannelCredentials.Insecure);
            return channel;
        }


        /// <summary>开始同步Master中Leader的任务
        /// </summary>
        private void StartGrpcSyncMasterLeaderTask()
        {
            _scheduleService.StartTask(SeaweedfsConsts.ScheduleTaskName.GrpcSyncMasterLeader, GrpcSyncMasterLeader, 1000, _option.GrpcOption.SyncGrpcMasterLeaderInterval * 1000);
        }

        /// <summary>结束同步Master中Leader的任务
        /// </summary>
        private void StopGrpcSyncMasterLeaderTask()
        {
            _scheduleService.StopTask(SeaweedfsConsts.ScheduleTaskName.GrpcSyncMasterLeader);
        }

        /// <summary>同步集群中MasterLeader
        /// </summary>
        private async void GrpcSyncMasterLeader()
        {
            if (_masterLeaderChannel == null)
            {
                _logger.LogError("同步MasterLeader出错,当前MasterLeaderChannel为空.");
                return;
            }

            try
            {
                var client = GetMasterClient();
                var request = new MasterPb.KeepConnectedRequest()
                {
                    Name = Guid.NewGuid().ToString("N")
                };

                using (var call = client.KeepConnected())
                {
                    var responseReaderTask = Task.Run(() =>
                    {
                        while (call.ResponseStream.MoveNext(CancellationToken.None).WaitResult(5000))
                        {
                            var volumeLocation = call.ResponseStream.Current;

                            //接收到VolumeLocation信息后处理...
                            if (!volumeLocation.Leader.IsNullOrWhiteSpace() && !volumeLocation.Leader.Equals(_masterLeaderChannel.Target, StringComparison.OrdinalIgnoreCase))
                            {
                                //不相等,创建新的连接
                                lock (SyncObject)
                                {
                                    var connectionAddress = new ConnectionAddress(volumeLocation.Leader);
                                    _masterLeaderChannel = CreateChannel(connectionAddress);
                                }
                            }
                        }
                    });
                    await call.RequestStream.WriteAsync(request);
                    await call.RequestStream.CompleteAsync();
                    await responseReaderTask;
                }
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    _logger.LogError(e.InnerException, "同步MasterLeader出现线程异常,{0}", ex.Message);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "同步MasterLeader出错,{0}", ex.Message);
            }

        }
    }
}
