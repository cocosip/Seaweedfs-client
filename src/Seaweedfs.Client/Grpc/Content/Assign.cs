namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>分配Fid
    /// </summary>
    public class Assign
    {
        /// <summary>文件Fid
        /// </summary>
        public string Fid { get; set; }

        /// <summary>地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>外部地址
        /// </summary>
        public string PublicUrl { get; set; }

        /// <summary>数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>错误信息
        /// </summary>
        public string Error { get; set; }

        /// <summary>认证信息
        /// </summary>
        public string Auth { get; set; }
    }
}
