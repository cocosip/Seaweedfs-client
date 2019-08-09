using Seaweedfs.Client.Extensions;
namespace Seaweedfs.Client.Rest
{
    /// <summary>上传文件请求
    /// </summary>
    public class UploadFileRequest : ISeaweedfsRequest<UploadFileResponse>
    {
        /// <summary>文件Id
        /// </summary>
        public string Fid { get; set; }

        /// <summary>文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>文件二进制
        /// </summary>
        public byte[] FileBytes { get; set; }

        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Fid, Method.POST);
            if (!FilePath.IsNullOrWhiteSpace())
            {
                builder.AddFile("file", FilePath);
            }
            if (FileBytes != null && FileBytes.Length > 0)
            {
                builder.AddFile("file", FileBytes, "");
            }

            return builder;
        }
    }
}
