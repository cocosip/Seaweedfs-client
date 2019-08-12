using System;
using System.IO;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Seaweedfs客户端
    /// </summary>
    public interface ISeaweedfsClient
    {
        /// <summary>获取AssignFid
        /// </summary>
        /// <param name="replication">同步机制</param>
        /// <param name="dataCenter">数据中心</param>
        /// <param name="ttl">时效</param>
        /// <param name="collection">集合</param>
        /// <param name="count">数量</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.AssignFileKeyResponse"/></returns>
        Task<AssignFileKeyResponse> AssignFileKey(string replication = "", int? count = 1, string dataCenter = "", string ttl = "", string collection = "");

        /// <summary>查询Volume
        /// </summary>
        /// <param name="volumeId">VolumeId</param>
        /// <param name="collection">集合</param>
        /// <param name="fid">文件Id</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.LookupVolumeResponse"/></returns>
        Task<LookupVolumeResponse> LookupVolume(string volumeId = "", string collection = "", string fid = "");

        /// <summary>强制垃圾回收
        /// </summary>
        /// <param name="garbageThreshold">阈值</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.ForceGarbageCollectionResponse"/></returns>
        Task<ForceGarbageCollectionResponse> ForceGC(decimal? garbageThreshold = null);

        /// <summary>预分配Volumes
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="dataCenter">数据中心</param>
        /// <param name="replication">同步机制</param>
        /// <param name="ttl">时效</param>
        /// <param name="collection">集合</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.PreAllocateVolumesResponse"/></returns>
        Task<PreAllocateVolumesResponse> PreAllocateVolumes(int count = 1, string dataCenter = "", string replication = "", string ttl = "", string collection = "");

        /// <summary>删除集合
        /// </summary>
        /// <param name="collection">集合名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.DeleteCollectionResponse"/></returns>
        Task<DeleteCollectionResponse> DeleteCollection(string collection);

        /// <summary>查询集群状态
        /// </summary>
        /// <returns><see cref="Seaweedfs.Client.Rest.ClusterStatusResponse"/></returns>
        Task<ClusterStatusResponse> GetClusterStatus();

        /// <summary>查询系统状态
        /// </summary>
        /// <returns><see cref="Seaweedfs.Client.Rest.SystemStatusResponse"/></returns>
        Task<SystemStatusResponse> GetSystemStatus();

        /// <summary>上传文件
        /// </summary>
        /// <param name="assignFileKey">文件Fid</param>
        /// <param name="filePath">文件本地路径</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        Task<UploadFileResponse> UploadFile(AssignFileKey assignFileKey, string filePath);

        /// <summary>上传文件
        /// </summary>
        /// <param name="assignFileKey">文件Fid</param>
        /// <param name="fileBytes">文件二进制</param>
        /// <param name="fileName">文件名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        Task<UploadFileResponse> UploadFile(AssignFileKey assignFileKey, byte[] fileBytes, string fileName);

        /// <summary>上传文件
        /// </summary>
        /// <param name="assignFileKey">文件Fid</param>
        /// <param name="writer">文件写入方法</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentLength">文件长度</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileResponse"/></returns>
        Task<UploadFileResponse> UploadFile(AssignFileKey assignFileKey, Action<Stream> writer, string fileName, long contentLength);

        /// <summary>直接上传文件
        /// </summary>
        /// <param name="filePath">文件本地路径</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileDirectlyResponse"/></returns>
        Task<UploadFileDirectlyResponse> UploadFileDirectly(string filePath);

        /// <summary>直接上传文件
        /// </summary>
        /// <param name="fileBytes">文件二进制</param>
        /// <param name="fileName">文件名</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileDirectlyResponse"/></returns>
        Task<UploadFileDirectlyResponse> UploadFileDirectly(byte[] fileBytes, string fileName);

        /// <summary>直接上传文件
        /// </summary>
        /// <param name="writer">文件写入方法</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentLength">文件长度</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.UploadFileDirectlyResponse"/></returns>
        Task<UploadFileDirectlyResponse> UploadFileDirectly(Action<Stream> writer, string fileName, long contentLength);

        /// <summary>根据Fid删除文件
        /// </summary>
        /// <param name="fid">文件Id</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.DeleteFileResponse"/></returns>
        Task<DeleteFileResponse> DeleteFile(string fid);

        /// <summary>获取volume服务器状态
        /// </summary>
        /// <param name="url">volume服务器的地址,例如:127.0.0.1:8080</param>
        /// <returns><see cref="Seaweedfs.Client.Rest.VolumeServerStatusResponse"/></returns>
        Task<VolumeServerStatusResponse> GetVolumeServerStatus(string url);

        /// <summary>根据Fid获取下载地址
        /// </summary>
        /// <param name="fid">文件Id</param>
        /// <returns></returns>
        string GetDownloadUrl(string fid);
    }
}
