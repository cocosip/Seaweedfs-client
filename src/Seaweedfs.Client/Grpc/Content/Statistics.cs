namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>统计信息
    /// </summary>
    public class Statistics
    {
        /// <summary>Replication
        /// </summary>
        public string Replication { get; set; }

        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>Ttl
        /// </summary>
        public string Ttl { get; set; }

        /// <summary>总空间
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>已使用空间
        /// </summary>
        public long UsedSize { get; set; }

        /// <summary>文件数量
        /// </summary>
        public long FileCount { get; set; }
    }
}
