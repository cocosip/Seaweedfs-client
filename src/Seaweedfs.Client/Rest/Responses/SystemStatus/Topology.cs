using System;
using System.Collections.Generic;
using System.Text;

namespace Seaweedfs.Client.Rest
{
    /// <summary>拓扑
    /// </summary>
    public class Topology
    {
        /// <summary>空闲
        /// </summary>
        public int Free { get; set; }

        /// <summary>最大
        /// </summary>
        public int Max { get; set; }

        /// <summary>数据中心
        /// </summary>
        public List<DataCenter> DataCenters { get; set; } = new List<DataCenter>();

        /// <summary>编排
        /// </summary>
        public List<Layout> Layouts { get; set; } = new List<Layout>();
    }
}
