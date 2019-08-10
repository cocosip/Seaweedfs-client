using Seaweedfs.Client.Extensions;

namespace Seaweedfs.Client.Rest
{
    /// <summary>查询Volume请求
    /// </summary>
    public class LookupVolumeRequest : ISeaweedfsRequest<LookupVolumeResponse>
    {
        /// <summary>VolumeId
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
        public LookupVolumeRequest(string volumeId, string collection, string fid)
        {
            VolumeId = volumeId;
            Collection = collection;
            Fid = fid;
        }

        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/dir/lookup", Method.GET);
            if (!VolumeId.IsNullOrWhiteSpace())
            {
                builder.AddParameter("VolumeId", VolumeId, ParameterType.QueryString);
            }
            if (!Collection.IsNullOrWhiteSpace())
            {
                builder.AddParameter("Collection", Collection, ParameterType.QueryString);
            }
            if (!Fid.IsNullOrWhiteSpace())
            {
                builder.AddParameter("FileId", Fid, ParameterType.QueryString);
            }
            return builder;
        }
    }
}
