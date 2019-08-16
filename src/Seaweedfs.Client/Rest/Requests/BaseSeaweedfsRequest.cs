namespace Seaweedfs.Client.Rest
{
    /// <summary>Seaweedfs请求基类
    /// </summary>
    public abstract class BaseSeaweedfsRequest<T> : ISeaweedfsRequest<T> where T : SeaweedfsResponse
    {
        /// <summary>请求资源
        /// </summary>
        public abstract string Resource { get; set; }

        /// <summary>服务器端类型
        /// </summary>
        public abstract ServerType ServerType { get; set; }

        /// <summary>文件Fid
        /// </summary>
        public virtual string Fid { get; set; }

        /// <summary>创建HttpBuilder
        /// </summary>
        public abstract HttpBuilder CreateBuilder();
    }
}
