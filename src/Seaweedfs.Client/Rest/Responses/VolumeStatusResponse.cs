using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>查询Volume状态响应
    /// </summary>
    public class VolumeStatusResponse : SeaweedfsResponse
    {
        /// <summary>版本信息
        /// </summary>
        public string Version { get; set; }

        /// <summary>Volumes
        /// </summary>
        public List<Volume> Volumes { get; set; } = new List<Volume>();
    }
}
