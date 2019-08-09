namespace Seaweedfs.Client.Rest
{
    /// <summary>分配文件Key返回
    /// </summary>
    public class AssignFileKeyResponse : SeaweedfsResponse
    {
        /// <summary>数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>文件Fid
        /// </summary>
        public string Fid { get; set; }

        /// <summary>Url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>公共Url地址
        /// </summary>
        public string PublicUrl { get; set; }

    }

}
