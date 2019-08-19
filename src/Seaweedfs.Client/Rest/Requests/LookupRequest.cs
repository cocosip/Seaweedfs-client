namespace Seaweedfs.Client.Rest
{
    /// <summary>Lookup查询Jwt请求
    /// </summary>
    public class LookupRequest : BaseSeaweedfsRequest<LookupResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/dir/lookup";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Master;

        /// <summary>是否读文件
        /// </summary>
        public bool? IsRead { get; set; }

        /// <summary>Ctor
        /// </summary>
        public LookupRequest(string fid)
        {
            Fid = fid;
        }

        /// <summary>Ctor
        /// </summary>
        public LookupRequest(string fid, bool isRead) : this(fid)
        {
            IsRead = isRead;
        }

        /// <summary>创建HttpBuilder
        /// </summary>
        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.GET);
            builder.AddParameter("fileId", Fid, ParameterType.QueryString);
            if (IsRead.HasValue)
            {
                if (IsRead.Value)
                {
                    builder.AddParameter("read", "yes", ParameterType.QueryString);
                }
            }
            return builder;
        }
    }
}
