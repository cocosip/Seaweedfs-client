namespace Seaweedfs.Client.Rest
{
    /// <summary>系统状态返回
    /// </summary>
    public class SystemStatusResponse : SeaweedfsResponse
    {
        /// <summary>拓扑
        /// </summary>
        public Topology Topology { get; set; }

        /// <summary>版本信息
        /// </summary>
        public string Version { get; set; }
    }
}
