using System.Collections.Generic;

namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>Volume集合
    /// </summary>
    public class VolumeList
    {
        /// <summary>拓扑信息
        /// </summary>
        public Topology Topology { get; set; }

        /// <summary>Volume大小限制
        /// </summary>
        public long VolumeSizeLimitMb { get; set; }

    }

    /// <summary>拓扑信息
    /// </summary>
    public class Topology
    {
        /// <summary>Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>Volume数量
        /// </summary>
        public long VolumeCount { get; set; }

        /// <summary>Volume最大数量
        /// </summary>
        public long MaxVolumeCount { get; set; }

        /// <summary>空闲Volume数量
        /// </summary>
        public long FreeVolumeCount { get; set; }

        /// <summary>活动的Volume数量
        /// </summary>
        public long ActiveVolumeCount { get; set; }

        /// <summary>数据中心
        /// </summary>
        public List<DataCenter> DataCenters { get; set; } = new List<DataCenter>();

    }

    /// <summary>数据中心
    /// </summary>
    public class DataCenter
    {
        /// <summary>Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>Volume数量
        /// </summary>
        public long VolumeCount { get; set; }

        /// <summary>Volume最大数量
        /// </summary>
        public long MaxVolumeCount { get; set; }

        /// <summary>空闲Volume数量
        /// </summary>
        public long FreeVolumeCount { get; set; }

        /// <summary>活动的Volume数量
        /// </summary>
        public long ActiveVolumeCount { get; set; }

        /// <summary>机架
        /// </summary>
        public List<Rack> Racks { get; set; } = new List<Rack>();
    }

    /// <summary>机架信息
    /// </summary>
    public class Rack
    {
        /// <summary>Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>Volume数量
        /// </summary>
        public long VolumeCount { get; set; }

        /// <summary>Volume最大数量
        /// </summary>
        public long MaxVolumeCount { get; set; }

        /// <summary>空闲Volume数量
        /// </summary>
        public long FreeVolumeCount { get; set; }

        /// <summary>活动的Volume数量
        /// </summary>
        public long ActiveVolumeCount { get; set; }

        /// <summary>数据节点
        /// </summary>
        public List<DataNode> DataNodes { get; set; } = new List<DataNode>();
    }

    /// <summary>数据节点
    /// </summary>
    public class DataNode
    {
        /// <summary>Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>Volume数量
        /// </summary>
        public long VolumeCount { get; set; }

        /// <summary>Volume最大数量
        /// </summary>
        public long MaxVolumeCount { get; set; }

        /// <summary>空闲Volume数量
        /// </summary>
        public long FreeVolumeCount { get; set; }

        /// <summary>活动的Volume数量
        /// </summary>
        public long ActiveVolumeCount { get; set; }

        /// <summary>Volume信息
        /// </summary>
        public List<VolumeInformation> VolumeInfos { get; set; } = new List<VolumeInformation>();

        /// <summary>EcShar Volume信息
        /// </summary>
        public List<VolumeEcShardInformation> EcShareInfos { get; set; } = new List<VolumeEcShardInformation>();
    }

    /// <summary>Volume信息
    /// </summary>
    public class VolumeInformation
    {

        /// <summary>Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>文件数量
        /// </summary>
        public long FileCount { get; set; }

        /// <summary>删除的数量
        /// </summary>
        public long DeleteCount { get; set; }

        /// <summary>删除的二进制数量
        /// </summary>
        public long DeletedByteCount { get; set; }

        /// <summary>是否只读
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>备份数量
        /// </summary>
        public int ReplicaPlacement { get; set; }

        /// <summary>版本信息
        /// </summary>
        public int Version { get; set; }

        /// <summary>Ttl时间
        /// </summary>
        public int Ttl { get; set; }

        /// <summary>契约修正?
        /// </summary>
        public int CompactRevision { get; set; }

        /// <summary>修改后的秒数
        /// </summary>
        public long ModifiedAtSecond { get; set; }
    }

    /// <summary>VolumeEcShard信息
    /// </summary>
    public class VolumeEcShardInformation
    {
        /// <summary>Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>ec_index_bits
        /// </summary>
        public int EcIndexBits { get; set; }
    }
}
