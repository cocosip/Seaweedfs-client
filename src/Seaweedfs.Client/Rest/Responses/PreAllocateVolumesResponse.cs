namespace Seaweedfs.Client.Rest
{
    /// <summary>预分配Volume响应
    /// </summary>
    public class PreAllocateVolumesResponse : SeaweedfsResponse
    {
        /// <summary>数量
        /// </summary>
        public int Count { get; set; }
    }
}
