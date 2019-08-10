using System;
using System.IO;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Seaweedfs客户端
    /// </summary>
    public class SeaweedfsClient : ISeaweedfsClient
    {
        private readonly IConnectionManager _connectionManager;
        private readonly ISeaweedfsExecuter _seaweedfsExecuter;

        /// <summary>Ctor
        /// </summary>
        public SeaweedfsClient(IConnectionManager connectionManager, ISeaweedfsExecuter seaweedfsExecuter)
        {
            _connectionManager = connectionManager;
            _seaweedfsExecuter = seaweedfsExecuter;
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
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
        }

        /// <summary>查询Volume
        /// </summary>
        /// <param name="volumeId">VolumeId</param>
        /// <param name="collection">集合</param>
        /// <param name="fid">根据文件Id查询</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.LookupVolumeResponse"/></returns>
        public async Task<LookupVolumeResponse> LookupVolume(string volumeId = "", string collection = "", string fid = "")
        {
            var request = new LookupVolumeRequest(volumeId, collection, fid);
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
        }

        /// <summary>强制垃圾回收
        /// </summary>
        /// <param name="garbageThreshold">阈值</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.ForceGarbageCollectionResponse"/></returns>
        public async Task<ForceGarbageCollectionResponse> ForceGC(decimal? garbageThreshold = null)
        {
            var request = new ForceGarbageCollectionRequest(garbageThreshold);
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
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
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
        }

        /// <summary>删除集合
        /// </summary>
        /// <param name="collection">集合名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.DeleteCollectionResponse"/></returns>
        public async Task<DeleteCollectionResponse> DeleteCollection(string collection)
        {
            var request = new DeleteCollectionRequest(collection);
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
        }

        /// <summary>查询集群状态
        /// </summary>
        /// <returns><see cref="Seaweedfs.Client.Rest.ClusterStatusResponse"/></returns>
        public async Task<ClusterStatusResponse> GetClusterStatus()
        {
            var request = new ClusterStatusRequest();
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
        }

        /// <summary>上传文件
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="filePath">文件本地路径</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        public async Task<UploadFileResponse> UploadFile(string fid, string filePath)
        {
            var request = new UploadFileRequest(fid, filePath);
            return await UploadInternal(request);
        }

        /// <summary>上传文件
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="fileBytes">文件二进制</param>
        /// <param name="fileName">文件名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        public async Task<UploadFileResponse> UploadFile(string fid, byte[] fileBytes, string fileName)
        {
            var request = new UploadFileRequest(fid, fileBytes, fileName);
            return await UploadInternal(request);
        }


        /// <summary>上传文件
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="writer">文件写入方法</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentLength">文件长度</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        public async Task<UploadFileResponse> UploadFile(string fid, Action<Stream> writer, string fileName, long contentLength)
        {
            var request = new UploadFileRequest(fid, writer, fileName, contentLength);
            return await UploadInternal(request);
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


        /// <summary>上传文件内部实现方法
        /// </summary>
        private async Task<UploadFileResponse> UploadInternal(UploadFileRequest request)
        {
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
        }

        /// <summary>直接上传文件内部方法
        /// </summary>
        private async Task<UploadFileDirectlyResponse> UploadDirectlyInternal(UploadFileDirectlyRequest request)
        {
            var connection = _connectionManager.GetMasterConnection();
            return await _seaweedfsExecuter.ExecuteAsync(connection, request);
        }

    }
}
