using Microsoft.Extensions.Logging;
using RestSharp;
using Seaweedfs.Client.Util;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>文件下载器
    /// </summary>
    public class DefaultDownloader : IDownloader
    {
        private readonly object SyncObject = new object();
        private readonly ILogger _logger;
        private readonly SeaweedfsOption _option;
        private readonly ISeaweedfsExecuter _executer;
        private readonly IConnectionManager _connectionManager;
        private readonly ConcurrentDictionary<ConnectionAddress, IRestClient> _clientDict = new ConcurrentDictionary<ConnectionAddress, IRestClient>();

        /// <summary>Ctor
        /// </summary>
        public DefaultDownloader(ILoggerFactory loggerFactory, SeaweedfsOption option, ISeaweedfsExecuter executer, IConnectionManager connectionManager)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
            _option = option;
            _executer = executer;
            _connectionManager = connectionManager;
        }

        /// <summary>下载文件
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        public async Task<string> DownloadFileAsync(string fid, string savePath)
        {

            Action<Stream> writer = s =>
            {
                using (var fs = File.OpenWrite(savePath))
                {
                    s.CopyTo(fs);
                }
            };
            await DownloadInternal(fid, writer);
            return savePath;
        }

        /// <summary>下载文件
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="writer">写入操作</param>
        /// <returns></returns>
        public async Task DownloadFileAsync(string fid, Action<Stream> writer)
        {
            await DownloadInternal(fid, writer);
        }


        private async Task DownloadInternal(string fid, Action<Stream> writer)
        {
            ConnectionAddress connectionAddress;
            if (_option.RestOption.EnableReadJwt)
            {
                var lookupRequest = new LookupRequest(fid, true);
                var lookupResponse = await _executer.ExecuteAsync(lookupRequest);
                var url = lookupResponse.Locations.FirstOrDefault().Url;
                connectionAddress = new ConnectionAddress(url);
            }
            else
            {
                var connection = _connectionManager.GetVolumeConnectionByVolumeIdOrFid(fid);
                connectionAddress = connection.ConnectionAddress;
            }
            await Task.Factory.StartNew(() =>
            {
                var client = GetDownloadClient(connectionAddress);
                IRestRequest request = new RestRequest("/{fid}");
                request.AddUrlSegment("fid", fid);
                request.ResponseWriter = writer;
                var response = client.DownloadData(request);
            });

        }

        /// <summary>获取下载的客户端
        /// </summary>
        private IRestClient GetDownloadClient(ConnectionAddress connectionAddress)
        {
            if (!_clientDict.TryGetValue(connectionAddress, out IRestClient client))
            {
                lock (SyncObject)
                {

                    client = new RestClient(UrlUtil.ToUrl(_option.RestOption.Scheme, connectionAddress.IPAddress, connectionAddress.Port));
                    _clientDict.TryAdd(connectionAddress, client);
                }
            }
            return client;
        }

    }
}
