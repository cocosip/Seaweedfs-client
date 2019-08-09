using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>数据中心
    /// </summary>
    public class DataCenter
    {
        /// <summary>数据中心Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>空闲数量
        /// </summary>
        public long Free { get; set; }

        /// <summary>最大数量
        /// </summary>
        public long Max { get; set; }

        /// <summary>机架
        /// </summary>
        public List<Rack> Racks { get; set; } = new List<Rack>();

        /// <summary>Ctor
        /// </summary>
        public DataCenter()
        {

        }

    }
}
