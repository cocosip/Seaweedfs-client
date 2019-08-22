namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>批量删除结果
    /// </summary>
    public class BatchDelete
    {
        /// <summary>文件Id
        /// </summary>
        public string FileId { get; set; }

        /// <summary>状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>版本
        /// </summary>
        public int Version { get; set; }
    }
}
