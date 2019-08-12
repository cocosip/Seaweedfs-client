using Seaweedfs.Client.Extensions;

namespace Seaweedfs.Client.Rest
{
    /// <summary>查询Volume请求
    /// </summary>
    public class LookupVolumeRequest : ISeaweedfsRequest<LookupVolumeResponse>
    {
        /// <summary>VolumeId,可以是文件Fid
        /// </summary>
        public string VolumeId { get; set; }

        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }

        /// <summary>文件Id
        /// </summary>
        public string Fid { get; set; }


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
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/dir/lookup", Method.GET);
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
