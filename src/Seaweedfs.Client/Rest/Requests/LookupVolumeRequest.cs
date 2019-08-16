using Seaweedfs.Client.Extensions;

namespace Seaweedfs.Client.Rest
{
    /// <summary>查询Volume请求
    /// </summary>
    public class LookupVolumeRequest : BaseSeaweedfsRequest<LookupVolumeResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/dir/lookup";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Master;

        /// <summary>VolumeId,可以是文件Fid
        /// </summary>
        public string VolumeId { get; set; }

        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>Ctor
        /// </summary>
        public LookupVolumeRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public LookupVolumeRequest(string volumeId)
        {
            VolumeId = volumeId;
        }

        /// <summary>Ctor
        /// </summary>
        public LookupVolumeRequest(string volumeId, string collection) : this(volumeId)
        {
            Collection = collection;
        }

        /// <summary>Ctor
        /// </summary>
        public LookupVolumeRequest(string volumeId, string collection, string fid) : this(volumeId, collection)
        {
            Fid = fid;
        }

        /// <summary>创建HttpBuilder
        /// </summary>
        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.GET);
            if (!VolumeId.IsNullOrWhiteSpace())
            {
                builder.AddParameter("volumeId", VolumeId, ParameterType.QueryString);
            }
            if (!Collection.IsNullOrWhiteSpace())
            {
                builder.AddParameter("collection", Collection, ParameterType.QueryString);
            }
            return builder;
        }
    }
}
