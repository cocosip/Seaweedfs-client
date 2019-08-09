using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>布局,配置
    /// </summary>
    public class Layout
    {
        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>同步复制
        /// </summary>
        public string Replication { get; set; }

        /// <summary>生存周期
        /// </summary>
        public string Ttl { get; set; }

        /// <summary>可写Id
        /// </summary>
        public List<long> Writables { get; set; } = new List<long>();

        /// <summary>Ctor
        /// </summary>
        public Layout()
        {

        }
    }
}
