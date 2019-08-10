namespace Seaweedfs.Client.Rest
{

    /// <summary>直接上传文件返回
    /// </summary>
    public class UploadFileDirectlyResponse : SeaweedfsResponse
    {
        /// <summary>文件Fid
        /// </summary>
        public string Fid { get; set; }

        /// <summary>文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>文件Url地址
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>文件大小
        /// </summary>
        public long Size { get; set; }
    }
}
