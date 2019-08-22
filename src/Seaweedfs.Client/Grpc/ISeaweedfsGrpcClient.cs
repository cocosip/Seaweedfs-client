using Seaweedfs.Client.Grpc.Content;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Grpc
{
    /// <summary>Seaweedfs Grpc客户端
    /// </summary>
    public interface ISeaweedfsGrpcClient
    {
        /// <summary>获取AssignFid
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="replication">Replication</param>
        /// <param name="collection">Collection</param>
        /// <param name="ttl">时间</param>
        /// <param name="dataCenter">数据中心</param>
        /// <param name="rack">机架</param>
        /// <param name="dataNode">数据节点</param>
        /// <returns></returns>
        Task<Assign> AssignFileKey(int? count = 1, string replication = "", string collection = "", string ttl = "", string dataCenter = "", string rack = "", string dataNode = "");

        /// <summary>查询Volume
        /// </summary>
        /// <param name="collection">Collection</param>
        /// <param name="volumeIds">VolumeId集合</param>
        /// <returns></returns>
        Task<LookupVolume> LookupVolume(string collection = "", params string[] volumeIds);

        /// <summary>获取统计信息
        /// </summary>
        /// <param name="replication">Replication</param>
        /// <param name="collection">Collection</param>
        /// <param name="ttl">超时时间</param>
        /// <returns></returns>
        Task<Statistics> GetStatistics(string replication = "", string collection = "", string ttl = "");

        /// <summary>删除Collection
        /// </summary>
        /// <param name="name">Collection名称</param>
        /// <returns></returns>
        Task DeleteCollection(string name);

        /// <summary>获取系统状态
        /// </summary>
        /// <returns></returns>
        Task<VolumeList> GetVolumeList();

        /// <summary>获取Master配置
        /// </summary>
        Task<MasterConfiguration> GetMasterConfiguration();

        /// <summary>批量删除文件
        /// </summary>
        /// <param name="fileIds">文件Fid集合</param>
        /// <returns></returns>
        Task<List<BatchDelete>> BatchDelete(params string[] fileIds);
    }
}
