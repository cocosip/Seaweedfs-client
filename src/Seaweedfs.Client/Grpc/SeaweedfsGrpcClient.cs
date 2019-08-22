using Seaweedfs.Client.Grpc.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Seaweedfs.Client.Grpc
{
    /// <summary>Seaweedfs Grpc客户端
    /// </summary>
    public class SeaweedfsGrpcClient : ISeaweedfsGrpcClient
    {
        private readonly IGrpcClientManager _clientManager;

        /// <summary>Ctor
        /// </summary>
        public SeaweedfsGrpcClient(IGrpcClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        /// <summary>获取AssignFid
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="replication">Replication</param>
        /// <param name="collection">Collection</param>
        /// <param name="ttl">时间</param>
        /// <param name="dataCenter">数据中心</param>
        /// <param name="rack">机架</param>
        /// <param name="dataNode">数据节点</param>
        /// <returns></returns>
        public async Task<Assign> AssignFileKey(int? count = 1, string replication = "", string collection = "", string ttl = "", string dataCenter = "", string rack = "", string dataNode = "")
        {
            var client = _clientManager.GetMasterClient();
            var request = new MasterPb.AssignRequest()
            {
                Replication = replication,
                Collection = collection,
                Ttl = ttl,
                DataCenter = dataCenter,
                Rack = rack,
                DataNode = dataNode,
                Count = (ulong)count.Value,
            };
            var response = await client.AssignAsync(request);
            var assign = new Assign()
            {
                Fid = response.Fid,
                Url = response.Url,
                PublicUrl = response.PublicUrl,
                Count = (long)response.Count,
                Auth = response.Auth,
                Error = response.Error
            };
            return assign;
        }

        /// <summary>查询Volume
        /// </summary>
        /// <param name="volumeIds">VolumeId集合</param>
        /// <param name="collection">Collection</param>
        /// <returns></returns>
        public async Task<List<VolumeIdLocation>> LookupVolume(List<string> volumeIds = null, string collection = "")
        {
            var client = _clientManager.GetMasterClient();
            if (volumeIds == null)
            {
                volumeIds = new List<string>();
            }
            var request = new MasterPb.LookupVolumeRequest()
            {
                Collection = collection,
            };
            request.VolumeIds.Add(volumeIds);
            var response = await client.LookupVolumeAsync(request);
            var volumeIdLocations = new List<VolumeIdLocation>();
            foreach (var item in response.VolumeIdLocations)
            {
                volumeIdLocations.Add(new VolumeIdLocation()
                {
                    VolumeId = item.VolumeId,
                    Locations = item.Locations.Select(x => new Location(x.Url, x.PublicUrl)).ToList()
                });
            }
            return volumeIdLocations;
        }


        /// <summary>获取统计信息
        /// </summary>
        /// <param name="replication">Replication</param>
        /// <param name="collection">Collection</param>
        /// <param name="ttl">超时时间</param>
        /// <returns></returns>
        public async Task<Statistics> GetStatistics(string replication = "", string collection = "", string ttl = "")
        {
            var client = _clientManager.GetMasterClient();
            var request = new MasterPb.StatisticsRequest()
            {
                Replication = replication,
                Collection = collection,
                Ttl = ttl
            };
            var response = await client.StatisticsAsync(request);
            var statistics = new Statistics()
            {
                Collection = response.Collection,
                Replication = response.Replication,
                Ttl = response.Ttl,
                TotalSize = (long)response.TotalSize,
                FileCount = (long)response.FileCount,
                UsedSize = (long)response.UsedSize
            };
            return statistics;
        }

        /// <summary>删除Collection
        /// </summary>
        /// <param name="name">Collection名称</param>
        /// <returns></returns>
        public async Task DeleteCollection(string name)
        {
            var client = _clientManager.GetMasterClient();
            var request = new MasterPb.CollectionDeleteRequest()
            {
                Name = name
            };
            await client.CollectionDeleteAsync(request);
        }

        /// <summary>获取系统状态
        /// </summary>
        /// <returns></returns>
        public async Task<VolumeList> GetVolumeList()
        {
            var client = _clientManager.GetMasterClient();
            var response = await client.VolumeListAsync(new MasterPb.VolumeListRequest());

            Func<MasterPb.VolumeEcShardInformationMessage, VolumeEcShardInformation> ecShardFunc = e =>
            {
                return new VolumeEcShardInformation()
                {
                    Id = (int)e.Id,
                    Collection = e.Collection,
                    EcIndexBits = (int)e.EcIndexBits
                };
            };

            Func<MasterPb.VolumeInformationMessage, VolumeInformation> volumeFunc = v =>
            {
                return new VolumeInformation()
                {
                    Id = (int)v.Id,
                    Size = (long)v.Size,
                    Collection = v.Collection,
                    FileCount = (long)v.FileCount,
                    DeletedByteCount = (long)v.DeletedByteCount,
                    DeleteCount = (long)v.DeleteCount,
                    ReadOnly = v.ReadOnly,
                    ReplicaPlacement = (int)v.ReplicaPlacement,
                    Version = (int)v.Version,
                    Ttl = (int)v.Ttl,
                    CompactRevision = (int)v.CompactRevision,
                    ModifiedAtSecond = (long)v.ModifiedAtSecond
                };
            };

            Func<MasterPb.DataNodeInfo, DataNode> dataNodeFunc = d =>
            {
                return new DataNode()
                {
                    Id = d.Id,
                    VolumeCount = (long)d.VolumeCount,
                    MaxVolumeCount = (long)d.MaxVolumeCount,
                    FreeVolumeCount = (long)d.FreeVolumeCount,
                    ActiveVolumeCount = (long)d.ActiveVolumeCount,
                    EcShareInfos = d.EcShardInfos.Select(ecShardFunc).ToList(),
                    VolumeInfos = d.VolumeInfos.Select(volumeFunc).ToList()
                };
            };

            var volumeList = new VolumeList()
            {
                VolumeSizeLimitMb = (long)response.VolumeSizeLimitMb,
                Topology = new Topology()
                {
                    Id = response.TopologyInfo.Id,
                    VolumeCount = (long)response.TopologyInfo.VolumeCount,
                    MaxVolumeCount = (long)response.TopologyInfo.MaxVolumeCount,
                    FreeVolumeCount = (long)response.TopologyInfo.FreeVolumeCount,
                    ActiveVolumeCount = (long)response.TopologyInfo.ActiveVolumeCount,
                    DataCenters = response.TopologyInfo.DataCenterInfos.Select(x =>
                    {
                        var dataCenter = new DataCenter()
                        {
                            Id = x.Id,
                            VolumeCount = (long)x.VolumeCount,
                            MaxVolumeCount = (long)x.MaxVolumeCount,
                            FreeVolumeCount = (long)x.FreeVolumeCount,
                            ActiveVolumeCount = (long)x.ActiveVolumeCount,
                            Racks = x.RackInfos.Select(r =>
                            {
                                var rack = new Rack()
                                {
                                    Id = r.Id,
                                    VolumeCount = (long)r.VolumeCount,
                                    MaxVolumeCount = (long)r.MaxVolumeCount,
                                    FreeVolumeCount = (long)r.FreeVolumeCount,
                                    ActiveVolumeCount = (long)r.ActiveVolumeCount,
                                    DataNodes = r.DataNodeInfos.Select(dataNodeFunc).ToList()
                                };
                                return rack;
                            }).ToList()
                        };
                        return dataCenter;
                    }).ToList()
                }
            };

            return volumeList;
        }
    }
}
