using System.Collections.Generic;

namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>查询共享Volume输出
    /// </summary>
    public class LookupEcVolume
    {
        /// <summary>VolumeId
        /// </summary>
        public int VolumeId { get; set; }

        /// <summary>EcShardIdLocations
        /// </summary>
        public List<EcShardIdLocation> EcShardIdLocations { get; set; } = new List<EcShardIdLocation>();
    }

    /// <summary>共享Volume Location
    /// </summary>
    public class EcShardIdLocation
    {
        /// <summary>共享Id
        /// </summary>
        public int ShareId { get; set; }

        /// <summary>Locations
        /// </summary>
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
