namespace Seaweedfs.Client.Rest
{
    /// <summary>删除文件请求
    /// </summary>
    public class DeleteFileRequest : ISeaweedfsRequest<DeleteFileResponse>
    {
        /// <summary>删除文件
        /// </summary>
        public string Fid { get; set; }

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
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Fid, Method.DELETE);
            return builder;
        }
    }
}
