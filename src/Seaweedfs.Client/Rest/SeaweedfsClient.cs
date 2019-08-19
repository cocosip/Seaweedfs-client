using Seaweedfs.Client.Util;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Seaweedfs客户端
    /// </summary>
    public class SeaweedfsClient : ISeaweedfsClient
    {
        private readonly SeaweedfsOption _option;
        private readonly IConnectionManager _connectionManager;
        private readonly ISeaweedfsExecuter _seaweedfsExecuter;
        private readonly IDownloader _downloader;

        /// <summary>Ctor
        /// </summary>
        public SeaweedfsClient(IConnectionManager connectionManager, ISeaweedfsExecuter seaweedfsExecuter, SeaweedfsOption option, IDownloader downloader)
        {
            _option = option;
            _connectionManager = connectionManager;
            _seaweedfsExecuter = seaweedfsExecuter;
            _downloader = downloader;
        }

        /// <summary>获取AssignFid
        /// </summary>
        /// <param name="replication">同步机制</param>
        /// <param name="dataCenter">数据中心</param>
        /// <param name="ttl">时效</param>
        /// <param name="collection">集合</param>
        /// <param name="count">数量</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.AssignFileKeyResponse"/></returns>
        public async Task<AssignFileKeyResponse> AssignFileKey(string replication = "", int? count = 1, string dataCenter = "", string ttl = "", string collection = "")
        {
            var request = new AssignFileKeyRequest(replication, count, dataCenter, ttl, collection);
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>查询Volume
        /// </summary>
        /// <param name="volumeId">VolumeId</param>
        /// <param name="collection">集合</param>
        /// <param name="fid">文件Id</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.LookupVolumeResponse"/></returns>
        public async Task<LookupVolumeResponse> LookupVolume(string volumeId = "", string collection = "", string fid = "")
        {
            var request = new LookupVolumeRequest(volumeId, collection);
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>强制垃圾回收
        /// </summary>
        /// <param name="garbageThreshold">阈值</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.ForceGarbageCollectionResponse"/></returns>
        public async Task<ForceGarbageCollectionResponse> ForceGC(decimal? garbageThreshold = null)
        {
            var request = new ForceGarbageCollectionRequest(garbageThreshold);
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>预分配Volumes
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="dataCenter">数据中心</param>
        /// <param name="replication">同步机制</param>
        /// <param name="ttl">时效</param>
        /// <param name="collection">集合</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.PreAllocateVolumesResponse"/></returns>
        public async Task<PreAllocateVolumesResponse> PreAllocateVolumes(int count = 1, string dataCenter = "", string replication = "", string ttl = "", string collection = "")
        {
            var request = new PreAllocateVolumesRequest(count, dataCenter, replication, ttl, collection);
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>删除集合
        /// </summary>
        /// <param name="collection">集合名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.DeleteCollectionResponse"/></returns>
        public async Task<DeleteCollectionResponse> DeleteCollection(string collection)
        {
            var request = new DeleteCollectionRequest(collection);
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>查询集群状态
        /// </summary>
        /// <returns><see cref="Seaweedfs.Client.Rest.ClusterStatusResponse"/></returns>
        public async Task<ClusterStatusResponse> GetClusterStatus()
        {
            var request = new ClusterStatusRequest();
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }


        /// <summary>查询系统状态
        /// </summary>
        /// <returns><see cref="Seaweedfs.Client.Rest.SystemStatusResponse"/></returns>
        public async Task<SystemStatusResponse> GetSystemStatus()
        {
            var request = new SystemStatusRequest();
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>上传文件
        /// </summary>
        /// <param name="assignFileKey">文件Fid</param>
        /// <param name="filePath">文件本地路径</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        public async Task<UploadFileResponse> UploadFile(AssignFileKey assignFileKey, string filePath)
        {
            var request = new UploadFileRequest(assignFileKey.Fid, filePath);
            return await UploadInternal(assignFileKey, request);
        }

        /// <summary>上传文件
        /// </summary>
        /// <param name="assignFileKey">文件Fid</param>
        /// <param name="fileBytes">文件二进制</param>
        /// <param name="fileName">文件名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        public async Task<UploadFileResponse> UploadFile(AssignFileKey assignFileKey, byte[] fileBytes, string fileName)
        {
            var request = new UploadFileRequest(assignFileKey.Fid, fileBytes, fileName);
            return await UploadInternal(assignFileKey, request);
        }


        /// <summary>上传文件
        /// </summary>
        /// <param name="assignFileKey">文件Fid</param>
        /// <param name="writer">文件写入方法</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentLength">文件长度</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        public async Task<UploadFileResponse> UploadFile(AssignFileKey assignFileKey, Action<Stream> writer, string fileName, long contentLength)
        {
            var request = new UploadFileRequest(assignFileKey.Fid, writer, fileName, contentLength);
            return await UploadInternal(assignFileKey, request);
        }

        /// <summary>直接上传文件
        /// </summary>
        /// <param name="filePath">文件本地路径</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileDirectlyResponse"/></returns>
        public async Task<UploadFileDirectlyResponse> UploadFileDirectly(string filePath)
        {
            var request = new UploadFileDirectlyRequest(filePath);
            return await UploadDirectlyInternal(request);
        }

        /// <summary>直接上传文件
        /// </summary>
        /// <param name="fileBytes">文件二进制</param>
        /// <param name="fileName">文件名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileDirectlyResponse"/></returns>
        public async Task<UploadFileDirectlyResponse> UploadFileDirectly(byte[] fileBytes, string fileName)
        {
            var request = new UploadFileDirectlyRequest(fileBytes, fileName);
            return await UploadDirectlyInternal(request);
        }

        /// <summary>直接上传文件
        /// </summary>
        /// <param name="writer">文件写入方法</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentLength">文件长度</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileDirectlyResponse"/></returns>
        public async Task<UploadFileDirectlyResponse> UploadFileDirectly(Action<Stream> writer, string fileName, long contentLength)
        {
            var request = new UploadFileDirectlyRequest(writer, fileName, contentLength);
            return await UploadDirectlyInternal(request);
        }

        /// <summary>根据Fid删除文件
        /// </summary>
        /// <param name="fid">文件Id</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.DeleteFileResponse"/></returns>
        public async Task<DeleteFileResponse> DeleteFile(string fid)
        {
            if (_option.EnableJwt)
            {
                var lookupRequest = new LookupRequest(fid);
                await _seaweedfsExecuter.ExecuteAsync(lookupRequest);
            }
            var request = new DeleteFileRequest(fid);
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }


        /// <summary>获取volume服务器状态
        /// </summary>
        /// <param name="url">volume服务器的地址,例如:127.0.0.1:8080</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.VolumeServerStatusResponse"/></returns>
        public async Task<VolumeServerStatusResponse> GetVolumeServerStatus(string url)
        {
            var request = new VolumeServerStatusRequest();
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>根据Fid获取下载地址
        /// </summary>
        /// <param name="fid">文件Id</param>
        /// <returns></returns>
        public string GetDownloadUrl(string fid)
        {
            var connection = _connectionManager.GetMasterConnection();
            var connectionAddress = connection.ConnectionAddress;
            var masterUrl = UrlUtil.ToUrl(_option.Scheme, connectionAddress.IPAddress, connectionAddress.Port);
            return $"{masterUrl}/{fid}";
        }

        /// <summary>下载文件到指定目录
        /// </summary>
        /// <param name="fid">文件Id</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        public async Task DownloadFile(string fid, string savePath)
        {
            await _downloader.DownloadFileAsync(fid, savePath);
        }

        /// <summary>下载文件到流中,对流进行操作
        /// </summary>
        /// <param name="fid">文件Id</param>
        /// <param name="writer">写流操作</param>
        /// <returns></returns>
        public async Task DownloadFile(string fid, Action<Stream> writer)
        {
            await _downloader.DownloadFileAsync(fid, writer);
        }


        /// <summary>上传文件内部实现方法
        /// </summary>
        private async Task<UploadFileResponse> UploadInternal(AssignFileKey assignFileKey, UploadFileRequest request)
        {
            //指定上传服务器地址
            request.AssignServer = assignFileKey.Url;
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

        /// <summary>直接上传文件内部方法
        /// </summary>
        private async Task<UploadFileDirectlyResponse> UploadDirectlyInternal(UploadFileDirectlyRequest request)
        {
            return await _seaweedfsExecuter.ExecuteAsync(request);
        }

    }
}
