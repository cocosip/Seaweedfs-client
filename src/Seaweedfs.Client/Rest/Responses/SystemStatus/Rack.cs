using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>机架
    /// </summary>
    public class Rack
    {
        /// <summary>Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>空闲数量
        /// </summary>
        public long Free { get; set; }

        /// <summary>最大数量
        /// </summary>
        public long Max { get; set; }

        /// <summary>数据节点
        /// </summary>
        public List<DataNode> DataNodes { get; set; } = new List<DataNode>();

        /// <summary>Ctor
        /// </summary>
        public Rack()
        {

        }

    }
}
