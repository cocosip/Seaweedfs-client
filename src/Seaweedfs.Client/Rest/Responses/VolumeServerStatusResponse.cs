using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>获取VolumeServer服务器状态返回
    /// </summary>
    public class VolumeServerStatusResponse : SeaweedfsResponse
    {
        /// <summary>版本信息
        /// </summary>
        public string Version { get; set; }

        /// <summary>Volumes
        /// </summary>
        public List<Volume> Volumes { get; set; } = new List<Volume>();
    }
}
