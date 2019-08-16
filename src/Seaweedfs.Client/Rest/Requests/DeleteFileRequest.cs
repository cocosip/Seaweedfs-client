namespace Seaweedfs.Client.Rest
{
    /// <summary>删除文件请求
    /// </summary>
    public class DeleteFileRequest : BaseSeaweedfsRequest<DeleteFileResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/{fid}";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Volume;
        /// <summary>Ctor
        /// </summary>
        public DeleteFileRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public DeleteFileRequest(string fid)
        {
            Fid = fid;
        }

        /// <summary>创建HttpBuilder
        /// </summary>
        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.DELETE);
            builder.AddParameter("fid", Fid, ParameterType.UrlSegment);
            return builder;
        }
    }
}
