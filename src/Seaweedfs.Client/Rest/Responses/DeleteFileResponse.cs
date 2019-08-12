namespace Seaweedfs.Client.Rest
{
    /// <summary>删除文件响应
    /// </summary>
    public class DeleteFileResponse : SeaweedfsResponse
    {
        /// <summary>文件大小
        /// </summary>
        public int Size { get; set; }
    }
}
