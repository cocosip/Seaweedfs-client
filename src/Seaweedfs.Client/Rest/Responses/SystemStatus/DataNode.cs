namespace Seaweedfs.Client.Rest
{
    /// <summary>数据节点
    /// </summary>
    public class DataNode
    {
        /// <summary>EcShards
        /// </summary>
        public int EcShards { get; set; }

        /// <summary>空闲数量
        /// </summary>
        public long Free { get; set; }

        /// <summary>最大数量
        /// </summary>
        public long Max { get; set; }

        /// <summary>公共Url
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>Volume数量
        /// </summary>
        public int Volumes { get; set; }

        /// <summary>Ctor
        /// </summary>
        public DataNode()
        {

        }
    }
}
