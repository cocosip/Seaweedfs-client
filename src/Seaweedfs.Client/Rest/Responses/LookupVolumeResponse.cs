using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>查询Volume响应
    /// </summary>
    public class LookupVolumeResponse : SeaweedfsResponse
    {
        /// <summary>VolumeId
        /// </summary>
        public string VolumeId { get; set; }

        /// <summary>Locations
        /// </summary>
        public List<Location> Locations { get; set; } = new List<Location>();
    }

    /// <summary>Location
    /// </summary>
    public class Location
    {
        /// <summary>Url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>PublicUrl
        /// </summary>
        public string PublicUrl { get; set; }
    }
}
