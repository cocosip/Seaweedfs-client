using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>查询集群状态响应
    /// </summary>
    public class ClusterStatusResponse : SeaweedfsResponse
    {
        /// <summary>是否为地址
        /// </summary>
        public bool IsLeader { get; set; }

        /// <summary>Leader地址
        /// </summary>
        public string Leader { get; set; }

        /// <summary>同级其他的Master
        /// </summary>
        public List<string> Peers { get; set; } = new List<string>();

    }
}
