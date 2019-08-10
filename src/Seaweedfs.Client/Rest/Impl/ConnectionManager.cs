using Microsoft.Extensions.Logging;
using Seaweedfs.Client.Scheduling;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Seaweedfs.Client.Rest
{
    /// <summary>连接管理器
    /// </summary>
    public class ConnectionManager : IConnectionManager
    {
        private bool _isRunning = false;
        private object SyncObject = new object();
        private readonly ILogger _logger;
        private readonly IScheduleService _scheduleService;
        private readonly SeaweedfsOption _option;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ISeaweedfsExecuter _seaweedfsExecuter;
        private Connection _masterConnection;

        /// <summary>Volume连接
        /// </summary>
        private readonly ConcurrentDictionary<ConnectionAddress, Connection> _volumeConnections = new ConcurrentDictionary<ConnectionAddress, Connection>();
        /// <summary>Filer连接
        /// </summary>
        private readonly ConcurrentDictionary<ConnectionAddress, Connection> _filerConnections = new ConcurrentDictionary<ConnectionAddress, Connection>();

        /// <summary>Ctor
        /// </summary>
        public ConnectionManager(ILoggerFactory loggerFactory, IScheduleService scheduleService, SeaweedfsOption option, IConnectionFactory connnectionFactory, ISeaweedfsExecuter seaweedfsExecuter)
        {
            _logger = loggerFactory.CreateLogger(option.LoggerName);
            _scheduleService = scheduleService;
            _option = option;
            _connectionFactory = connnectionFactory;
            _seaweedfsExecuter = seaweedfsExecuter;
        }

        /// <summary>获取Master连接
        /// </summary>
        public Connection GetMasterConnection()
        {
            return _masterConnection;
        }


        /// <summary>运行
        /// </summary>
        public void Start()
        {
            if (_isRunning)
            {
                return;
            }

            //使用第一台服务器作为Leader
            _masterConnection = CreateDefaultMasterConnection();
            //Master Leader同步
            StartSyncMasterLeaderTask();
        }


        /// <summary>停止
        /// </summary>
        public void Shutdown()
        {
            StopSyncMasterLeaderTask();
        }


        /// <summary>创建默认的Master连接
        /// </summary>
        private Connection CreateDefaultMasterConnection()
        {
            var master = _option.Masters.FirstOrDefault();
            if (master == null)
            {
                throw new ArgumentException("配置文件中不包含任何Master节点的配置.");
            }

            var connectionAddress = new ConnectionAddress()
            {
                IPAddress = master.IPAddress,
                Port = master.Port
            };
            return _connectionFactory.CreateConnection(connectionAddress, ConnectionType.Master);
        }

        /// <summary>开始同步Master中Leader的任务
        /// </summary>
        private void StartSyncMasterLeaderTask()
        {
            _scheduleService.StartTask($"{SeaweedfsConsts.Seaweedfs}.SyncMasterLeader", SyncMasterLeader, 1000, _option.SyncMasterLeaderInterval);
        }

        /// <summary>结束同步Master中Leader的任务
        /// </summary>
        private void StopSyncMasterLeaderTask()
        {
            _scheduleService.StopTask($"{SeaweedfsConsts.Seaweedfs}.SyncMasterLeader");
        }

        /// <summary>同步集群中MasterLeader
        /// </summary>
        private void SyncMasterLeader()
        {
            if (_masterConnection == null)
            {
                _logger.LogError("同步MasterLeader出错,当前MasterConnection为空.");
                return;
            }
            try
            {
                //查询集群中的状态
                var request = new ClusterStatusRequest();
                //执行结果
                var task = _seaweedfsExecuter.ExecuteAsync(_masterConnection, request);
                task.Wait();
                //集群状态
                var clusterStatus = task.Result;
                //判断集群中的Master服务器的Leader是否发生了改变
                if (!clusterStatus.IsLeader || clusterStatus.Leader != _masterConnection.ConnectionAddress.ToString())
                {
                    //更新当前Master连接
                    lock(SyncObject)
                    {
                        var connectionAddress = new ConnectionAddress(clusterStatus.Leader);
                        _masterConnection = _connectionFactory.CreateConnection(connectionAddress, ConnectionType.Master);
                    }
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
