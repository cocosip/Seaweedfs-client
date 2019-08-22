using System.Collections.Generic;

namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>查询Volume
    /// </summary>
    public class LookupVolume
    {
        /// <summary>VolumeIdLocations
        /// </summary>
        public List<VolumeIdLocation> VolumeIdLocations { get; set; } = new List<VolumeIdLocation>();
    }

    /// <summary>VolumeId位置
    /// </summary>
    public class VolumeIdLocation
    {
        /// <summary>VolumeId
        /// </summary>
        public string VolumeId { get; set; }

        /// <summary>Locations
        /// </summary>
        public List<Location> Locations { get; set; } = new List<Location>();
    }

    /// <summary>位置
    /// </summary>
    public class Location
    {
        /// <summary>Url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>PublicUrl
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>Ctor
        /// </summary>
        public Location()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public Location(string url, string publicUrl)
        {
            Url = url;
            PublicUrl = publicUrl;
        }
    }
}
