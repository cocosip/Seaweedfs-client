using Microsoft.Extensions.Logging;
using RestSharp;
using Seaweedfs.Client.Util;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>文件下载器
    /// </summary>
    public class DefaultDownloader : IDownloader
    {
        private readonly object SyncObject = new object();
        private readonly ILogger _logger;

        private readonly ConcurrentDictionary<ConnectionAddress, IRestClient> _clientDict = new ConcurrentDictionary<ConnectionAddress, IRestClient>();

        /// <summary>Ctor
        /// </summary>
        public DefaultDownloader(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
        }

        /// <summary>异步下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="savePath">文件保存路径</param>
        /// <returns></returns>
        public Task<string> DownloadFileAsync(string url, string savePath)
        {
            _logger.LogDebug("【下载文件】url地址:{0},保存路径:{1}", url, savePath);
            return Task.Factory.StartNew(() =>
            {
                var client = GetDownloadClient(url);
                var request = BuildRequestFromUrl(url);
                using (var writer = File.OpenWrite(savePath))
                {
                    request.ResponseWriter = responseStream =>
                    {
                        using (responseStream)
                        {
                            responseStream.CopyTo(writer);
                        }
                    };
                    var response = client.DownloadData(request);
                }
                return savePath;
            });
        }

        /// <summary>异步下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="writer">流文件操作</param>
        /// <returns></returns>
        public Task DownloadFileAsync(string url, Action<Stream> writer)
        {
            _logger.LogDebug("【下载文件】url地址:{0}");
            return Task.Factory.StartNew(() =>
            {
                var client = GetDownloadClient(url);
                var request = BuildRequestFromUrl(url);
                request.ResponseWriter = writer;
                var response = client.DownloadData(request);
            });
        }

        /// <summary>根据url地址获取下载的客户端
        /// </summary>
        private IRestClient GetDownloadClient(string url)
        {
            var connectionAddress = new ConnectionAddress(UrlUtil.GetUrlAddress(url));
            if (!_clientDict.TryGetValue(connectionAddress, out IRestClient client))
            {
                lock (SyncObject)
                {
                    client = new RestClient(UrlUtil.GetBaseUrl(url));
                    _clientDict.TryAdd(connectionAddress, client);
                }
            }
            return client;
        }

        /// <summary>根据url构建请求
        /// </summary>
        private IRestRequest BuildRequestFromUrl(string url)
        {
            var uri = new Uri(url);
            IRestRequest request = new RestRequest(uri.LocalPath);
            //添加query
            var querys = UrlUtil.GetQuerys(url);
            foreach (var query in querys)
            {
                request.AddQueryParameter(query.Key, query.Value);
            }
            return request;
        }

    }
}
