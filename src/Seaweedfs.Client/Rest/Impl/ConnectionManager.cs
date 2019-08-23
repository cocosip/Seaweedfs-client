using Microsoft.Extensions.Logging;
using Seaweedfs.Client.Scheduling;
using Seaweedfs.Client.Util;
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
        private readonly object SyncObject = new object();
        private readonly ILogger _logger;
        private readonly IScheduleService _scheduleService;
        private readonly SeaweedfsOption _option;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ISeaweedfsExecuter _seaweedfsExecuter;
        private Connection _masterLeaderConnection = null;

        /// <summary>Volume连接
        /// </summary>
        private readonly ConcurrentDictionary<ConnectionAddress, Connection> _volumeConnections = new ConcurrentDictionary<ConnectionAddress, Connection>();

        /// <summary>Filer连接
        /// </summary>
        private readonly ConcurrentDictionary<ConnectionAddress, Connection> _filerConnections = new ConcurrentDictionary<ConnectionAddress, Connection>();

        /// <summary>VolumeId与地址的映射集合
        /// </summary>
        private readonly ConcurrentDictionary<string, ConnectionAddress> _volumeIdAddressMappers = new ConcurrentDictionary<string, ConnectionAddress>();

        /// <summary>Ctor
        /// </summary>
        public ConnectionManager(ILoggerFactory loggerFactory, IScheduleService scheduleService, SeaweedfsOption option, IConnectionFactory connnectionFactory, ISeaweedfsExecuter seaweedfsExecuter)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
            _scheduleService = scheduleService;
            _option = option;
            _connectionFactory = connnectionFactory;
            _seaweedfsExecuter = seaweedfsExecuter;
        }

        /// <summary>获取Master连接
        /// </summary>
        public Connection GetMasterConnection()
        {
            return _masterLeaderConnection;
        }

        /// <summary>根据AssignFileKeyResponse获取Volume连接
        /// </summary>
        public Connection GetVolumeConnectionByAssignFileKey(AssignFileKey assignFileKey)
        {
            return GetVolumeConnectionInternal(new ConnectionAddress(assignFileKey.Url));
        }

        /// <summary>根据VolumeId(可以是Fid)获取Volume连接
        /// </summary>
        public Connection GetVolumeConnectionByVolumeIdOrFid(string volumeIdOrFid)
        {
            //如果是Fid,就转换成VolumeId
            if (StringUtil.IsFid(volumeIdOrFid))
            {
                volumeIdOrFid = StringUtil.GetVolumeId(volumeIdOrFid);
            }

            if (_volumeIdAddressMappers.TryGetValue(volumeIdOrFid, out ConnectionAddress connectionAddress))
            {
                return GetVolumeConnectionInternal(connectionAddress);
            }
            else
            {

                var request = new LookupVolumeRequest(volumeIdOrFid);
                var task = _seaweedfsExecuter.ExecuteAsync(request);
                task.Wait();
                //查询Volume返回
                var lookupVolumeResponse = task.Result;
                if (lookupVolumeResponse.IsSuccessful)
                {
                    //成功
                    lock (SyncObject)
                    {
                        //连接地址
                        connectionAddress = new ConnectionAddress(lookupVolumeResponse.Locations.FirstOrDefault().Url);
                        //添加映射
                        _volumeIdAddressMappers.TryAdd(volumeIdOrFid, connectionAddress);
                        return GetVolumeConnectionInternal(connectionAddress);
                    }
                }
                else
                {
                    _logger.LogInformation("根据VolumeId获取Volume信息时出错,VolumeId:{0},HttpStatus:{1}", volumeIdOrFid, lookupVolumeResponse.StatusCode);
                    throw new Exception($"获取Volume信息出错,{lookupVolumeResponse.ErrorMessage}");
                }
            }

        }

        /// <summary>根据Url获取Volume连接
        /// </summary>
        public Connection GetVolumeConnectionByUrl(string url)
        {
            var connectionAddress = new ConnectionAddress(url);
            return GetVolumeConnectionInternal(connectionAddress);
        }

        /// <summary>根据链接地址获取Volume连接
        /// </summary>
        private Connection GetVolumeConnectionInternal(ConnectionAddress connectionAddress)
        {
            var volumeConnection = _volumeConnections.GetOrAdd(connectionAddress, addr =>
            {
                return _connectionFactory.CreateConnection(addr, ConnectionType.Volume);
            });
            return volumeConnection;
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
            _masterLeaderConnection = CreateMasterConnection();
            //Master Leader同步
            StartSyncMasterLeaderTask();
            _isRunning = true;
        }


        /// <summary>停止
        /// </summary>
        public void Shutdown()
        {
            StopSyncMasterLeaderTask();
            _isRunning = false;
        }


        /// <summary>创建默认的Master连接
        /// </summary>
        private Connection CreateMasterConnection(ConnectionAddress exceptMaster = null)
        {
            var masterServers = _option.RestOption.Masters;
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
            return _connectionFactory.CreateConnection(connectionAddress, ConnectionType.Master);
        }

        /// <summary>开始同步Master中Leader的任务
        /// </summary>
        private void StartSyncMasterLeaderTask()
        {
            _scheduleService.StartTask(SeaweedfsConsts.ScheduleTaskName.SyncMasterLeader, SyncMasterLeader, 1000, _option.RestOption.SyncMasterLeaderInterval * 1000);
        }

        /// <summary>结束同步Master中Leader的任务
        /// </summary>
        private void StopSyncMasterLeaderTask()
        {
            _scheduleService.StopTask(SeaweedfsConsts.ScheduleTaskName.SyncMasterLeader);
        }

        /// <summary>同步集群中MasterLeader
        /// </summary>
        private void SyncMasterLeader()
        {
            if (_masterLeaderConnection == null)
            {
                _logger.LogError("同步MasterLeader出错,当前MasterConnection为空.");
                return;
            }
            try
            {
                //查询集群中的状态
                var request = new ClusterStatusRequest();
                //执行结果
                var task = _seaweedfsExecuter.ExecuteAsync(request);
                task.Wait();
                //集群状态
                var clusterStatus = task.Result;
                if (!clusterStatus.IsSuccessful)
                {
                    //如果不成功,Master可能废了,需要构建新的
                    _masterLeaderConnection = CreateMasterConnection(_masterLeaderConnection.ConnectionAddress);
                    SyncMasterLeader();
                    return;
                }
                //判断集群中的Master服务器的Leader是否发生了改变
                if (!clusterStatus.IsLeader || clusterStatus.Leader != _masterLeaderConnection.ConnectionAddress.ToString())
                {
                    //更新当前Master连接
                    lock (SyncObject)
                    {
                        var connectionAddress = new ConnectionAddress(clusterStatus.Leader);
                        _masterLeaderConnection = _connectionFactory.CreateConnection(connectionAddress, ConnectionType.Master);
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
