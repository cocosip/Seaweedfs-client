using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Lookup查询Jwt响应
    /// </summary>
    public class LookupResponse : SeaweedfsResponse
    {
        /// <summary>VolumeId
        /// </summary>
        public string VolumeId { get; set; }

        /// <summary>Locations
        /// </summary>
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
