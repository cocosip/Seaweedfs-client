using System.Collections.Generic;

namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>VolumeLocation
    /// </summary>
    public class VolumeLocation
    {
        /// <summary>Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>PublicUrl
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>新增VolumeIds
        /// </summary>
        public List<int> NewVids { get; set; } = new List<int>();

        /// <summary>删除的VolumeIds
        /// </summary>
        public List<int> DeleteVids { get; set; } = new List<int>();

        /// <summary>Leader
        /// </summary>
        public string Leader { get; set; }
    }
}
