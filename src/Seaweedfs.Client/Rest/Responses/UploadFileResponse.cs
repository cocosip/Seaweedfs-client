namespace Seaweedfs.Client.Rest
{
    /// <summary>上传文件返回
    /// </summary>
    public class UploadFileResponse : SeaweedfsResponse
    {
        /// <summary>文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>标签
        /// </summary>
        public string ETag { get; set; }
    }
}
