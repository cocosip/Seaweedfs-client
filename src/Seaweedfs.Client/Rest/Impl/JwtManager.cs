using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Jwt管理
    /// </summary>
    public class JwtManager : IJwtManager
    {
        private readonly ILogger _logger;

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
        public JwtManager(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
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
    }
}
