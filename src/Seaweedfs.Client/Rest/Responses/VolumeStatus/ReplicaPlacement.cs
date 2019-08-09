namespace Seaweedfs.Client.Rest
{
    /// <summary>副本放置
    /// </summary>
    public class ReplicaPlacement
    {
        /// <summary>相同机架数量
        /// </summary>
        public long SameRackCount { get; set; }

        /// <summary>不同机架数量
        /// </summary>
        public long DiffRackCount { get; set; }

        /// <summary>不同数据中心数量
        /// </summary>
        public long DiffDataCenterCount { get; set; }

        /// <summary>Ctor
        /// </summary>
        public ReplicaPlacement()
        {

        }
    }
}
