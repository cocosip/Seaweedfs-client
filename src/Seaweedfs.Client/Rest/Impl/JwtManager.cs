using Microsoft.Extensions.Logging;
using Seaweedfs.Client.Scheduling;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Jwt管理
    /// </summary>
    public class JwtManager : IJwtManager
    {
        private bool _isRunning = false;
        private readonly ILogger _logger;
        private readonly IScheduleService _scheduleService;
        private readonly SeaweedfsOption _option;

        /// <summary>通过Assign获取Jwt的集合
        /// </summary>
        private readonly ConcurrentDictionary<string, JwtToken> _assignJwtDict = new ConcurrentDictionary<string, JwtToken>();

        /// <summary>通过Lookup获取Jwt的集合
        /// </summary>
        private readonly ConcurrentDictionary<string, JwtToken> _lookupJwtDict = new ConcurrentDictionary<string, JwtToken>();

        /// <summary>读文件获取Jwt的集合
        /// </summary>
        private readonly ConcurrentDictionary<string, JwtToken> _readAccessJwtDict = new ConcurrentDictionary<string, JwtToken>();

        /// <summary>Ctor
        /// </summary>
        public JwtManager(ILoggerFactory loggerFactory, IScheduleService scheduleService, SeaweedfsOption option)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
            _scheduleService = scheduleService;
            _option = option;
        }

        /// <summary>添加Assign Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="jwt">Jwt的值</param>
        public void AddAssignJwt(string fid, string jwt)
        {
            try
            {
                //添加或者修改JwtToken
                _assignJwtDict.AddOrUpdate(fid, new JwtToken(fid, jwt), (f, j) =>
                {
                    _logger.LogDebug("添加Assign Jwt,覆盖JwtToken,Fid:{0},原Jwt:{1},新Jwt:{2}", f, j.Jwt, jwt);
                    return new JwtToken(fid, jwt);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加Assign Jwt失败,{0}", ex.Message);
            }
        }

        /// <summary>根据Fid获取Assign Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <returns></returns>
        public string GetAssignJwt(string fid)
        {
            if (_assignJwtDict.TryGetValue(fid, out JwtToken jwtToken))
            {
                _logger.LogError("获取Assign Jwt失败,无法找到Fid为:{0} 的Jwt", fid);
            }
            return jwtToken?.Jwt;
        }


        /// <summary>添加Lookup Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="jwt">Jwt的值</param>
        public void AddLookupJwt(string fid, string jwt)
        {
            try
            {
                //添加或者修改JwtToken
                _lookupJwtDict.AddOrUpdate(fid, new JwtToken(fid, jwt), (f, j) =>
                {
                    _logger.LogDebug("添加Lookup Jwt,覆盖JwtToken,Fid:{0},原Jwt:{1},新Jwt:{2}", f, j.Jwt, jwt);
                    return new JwtToken(fid, jwt);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加Lookup Jwt失败,{0}", ex.Message);
            }
        }

        /// <summary>根据Fid获取Lookup Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <returns></returns>
        public string GetLookupJwt(string fid)
        {
            if (_lookupJwtDict.TryGetValue(fid, out JwtToken jwtToken))
            {
                _logger.LogError("获取Lookup Jwt失败,无法找到Fid为:{0} 的Jwt", fid);
            }
            return jwtToken?.Jwt;
        }

        /// <summary>添加读文件Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="jwt">Jwt的值</param>
        public void AddReadJwt(string fid, string jwt)
        {
            try
            {
                //添加或者修改JwtToken
                _readAccessJwtDict.AddOrUpdate(fid, new JwtToken(fid, jwt), (f, j) =>
                {
                    _logger.LogDebug("添加读文件Jwt,覆盖JwtToken,Fid:{0},原Jwt:{1},新Jwt:{2}", f, j.Jwt, jwt);
                    return new JwtToken(fid, jwt);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "添加读文件 Jwt失败,{0}", ex.Message);
            }
        }

        /// <summary>根据Fid获取Lookup Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <returns></returns>
        public string GetReadJwt(string fid)
        {
            if (_readAccessJwtDict.TryGetValue(fid, out JwtToken jwtToken))
            {
                _logger.LogError("获取Lookup Jwt失败,无法找到Fid为:{0} 的Jwt", fid);
            }
            return jwtToken?.Jwt;
        }

        /// <summary>运行
        /// </summary>
        public void Start()
        {
            if (_isRunning)
            {
                return;
            }
            //查询超时的
            if (_option.EnableJwt)
            {
                StartScanTimeoutJwtTask();
            }
            if (_option.EnableReadJwt)
            {
                StartScanTimeoutReadJwtTask();
            }
            _isRunning = true;
        }

        /// <summary>停止
        /// </summary>
        public void Shutdown()
        {
            if (_option.EnableJwt)
            {
                StopScanTimeoutJwtTask();
            }
            if (_option.EnableReadJwt)
            {
                StopScanTimeoutReadJwtTask();
            }
            _isRunning = false;
        }


        /// <summary>查询超时的Jwt并且移除任务
        /// </summary>
        private void StartScanTimeoutJwtTask()
        {
            _scheduleService.StartTask(SeaweedfsConsts.ScheduleTaskName.ScanTimeoutJwt, ScanTimeoutJwt, 1000, 1000);
        }

        /// <summary>停止查询超时的Jwt并且移除任务
        /// </summary>
        private void StopScanTimeoutJwtTask()
        {
            _scheduleService.StopTask(SeaweedfsConsts.ScheduleTaskName.ScanTimeoutJwt);
        }

        /// <summary>查询超时的Read Jwt并且移除任务
        /// </summary>
        private void StartScanTimeoutReadJwtTask()
        {
            _scheduleService.StartTask(SeaweedfsConsts.ScheduleTaskName.ScanTimeoutReadJwt, ScanTimeoutReadJwt, 1000, 1000);
        }

        /// <summary>停止查询超时的ReadJwt并且移除任务
        /// </summary>
        private void StopScanTimeoutReadJwtTask()
        {
            _scheduleService.StopTask(SeaweedfsConsts.ScheduleTaskName.ScanTimeoutReadJwt);
        }

        /// <summary>查询超时的Jwt
        /// </summary>
        private void ScanTimeoutJwt()
        {
            var assignTimeoutKeyList = new List<string>();
            foreach (var entry in _assignJwtDict)
            {
                if (entry.Value.IsTimeout(_option.JwtTimeoutSeconds))
                {
                    assignTimeoutKeyList.Add(entry.Key);
                }
            }
            foreach (var key in assignTimeoutKeyList)
            {
                if (_assignJwtDict.TryRemove(key, out JwtToken jwtToken))
                {
                    _logger.LogDebug("移除超时Jwt,Fid:{0},Token:{1}", key, jwtToken.Jwt);
                }
            }

            var lookUpTimeoutKeyList = new List<string>();
            foreach (var entry in _lookupJwtDict)
            {
                if (entry.Value.IsTimeout(_option.JwtTimeoutSeconds))
                {
                    lookUpTimeoutKeyList.Add(entry.Key);
                }
            }
            foreach (var key in lookUpTimeoutKeyList)
            {
                if (_lookupJwtDict.TryRemove(key, out JwtToken jwtToken))
                {
                    _logger.LogDebug("移除超时Jwt,Fid:{0},Token:{1}", key, jwtToken.Jwt);
                }
            }

        }


        /// <summary>查询超时的ReadJwt
        /// </summary>
        private void ScanTimeoutReadJwt()
        {
            var readJwtTimeoutKeyList = new List<string>();
            foreach (var entry in _readAccessJwtDict)
            {
                if (entry.Value.IsTimeout(_option.ReadJwtTimeoutSeconds))
                {
                    readJwtTimeoutKeyList.Add(entry.Key);
                }
            }
            foreach (var key in readJwtTimeoutKeyList)
            {
                if (_readAccessJwtDict.TryRemove(key, out JwtToken jwtToken))
                {
                    _logger.LogDebug("移除超时Jwt,Fid:{0},Token:{1}", key, jwtToken.Jwt);
                }
            }
        }
    }
}
