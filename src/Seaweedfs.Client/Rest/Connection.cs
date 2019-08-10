using Microsoft.Extensions.Logging;
using RestSharp;
using Seaweedfs.Client.Util;
using System;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>连接
    /// </summary>
    public class Connection
    {
        private readonly ILogger _logger;
        private readonly SeaweedfsOption _option;

        /// <summary>连接类型
        /// </summary>
        private ConnectionType _connectionType;
        /// <summary>连接地址
        /// </summary>
        public ConnectionAddress ConnectionAddress { get; private set; }


        /// <summary>连接Url地址
        /// </summary>
        public string BaseUrl { get; private set; }


        /// <summary>Rest客户端
        /// </summary>
        public IRestClient Client { get; private set; }

        /// <summary>Ctor
        /// </summary>
        public Connection(ILoggerFactory loggerFactory, SeaweedfsOption option, ConnectionAddress connectionAddress, ConnectionType connectionType)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
            _option = option;
            ConnectionAddress = connectionAddress;
            _connectionType = connectionType;
            BaseUrl = UrlUtil.ToUrl(option.Schema, connectionAddress.IPAddress, connectionAddress.Port);
            Client = new RestClient(BaseUrl);
        }

        /// <summary>执行RestRequest请求
        /// </summary>
        /// <param name="request">请求<see cref="Seaweedfs.Client.Rest.ISeaweedfsRequest"/></param>
        /// <returns></returns>
        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            try
            {
                return await Client.ExecuteTaskAsync(request).ConfigureAwait(false);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    _logger.LogError(e, "Connection执行RestRequest出现异常,{0}", e.Message);
                }
                throw new Exception("执行请求出错,详情参见日志");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection执行RestRequest出错,{0}", ex.Message);
                throw new Exception("执行请求出错,详情参见日志");
            }
        }

        /// <summary>释放资源
        /// </summary>
        public void Release()
        {
            Client = null;
            ConnectionAddress = null;
        }


    }



    /// <summary>连接类型
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>Master
        /// </summary>
        Master,
        /// <summary>Master
        /// </summary>
        Volume,
        /// <summary>Filer
        /// </summary>
        Filer


    }
}
