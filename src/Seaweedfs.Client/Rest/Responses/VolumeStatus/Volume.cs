namespace Seaweedfs.Client.Rest
{
    /// <summary>存储卷
    /// </summary>
    public class Volume
    {
        /// <summary>Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>已存储的大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>版本号
        /// </summary>
        public long Version { get; set; }

        /// <summary>文件数量
        /// </summary>
        public long FileCount { get; set; }

        /// <summary>删除的数量
        /// </summary>
        public long DeleteCount { get; set; }

        /// <summary>删除二进制数据的数量
        /// </summary>
        public long DeletedByteCount { get; set; }

        /// <summary>是否只读
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>契约版本
        /// </summary>
        public long CompactRevision { get; set; }

        /// <summary>修改的秒
        /// </summary>
        public long ModifiedAtSecond { get; set; }

        /// <summary>副本配置
        /// </summary>
        public ReplicaPlacement ReplicaPlacement { get; set; }

        /// <summary>Ttl数据
        /// </summary>
        public Ttl Ttl { get; set; }

        /// <summary>Ctor
        /// </summary>
        public Volume()
        {

        }

    }
}
