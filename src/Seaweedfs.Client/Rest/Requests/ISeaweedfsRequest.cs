namespace Seaweedfs.Client.Rest
{
    /// <summary>Seaweedfs请求基接口
    /// </summary>
    public interface ISeaweedfsRequest
    {
        /// <summary>请求资源
        /// </summary>
        string Resource { get; set; }

        /// <summary>服务器端类型
        /// </summary>
        ServerType ServerType { get; set; }

        /// <summary>指定服务器端地址
        /// </summary>
        string AssignServer { get; set; }

        /// <summary>文件Fid,部分接口有这个参数
        /// </summary>
        string Fid { get; set; }

        /// <summary>创建HttpBuilder
        /// </summary>
        HttpBuilder CreateBuilder();
    }

    /// <summary>Seaweedfs请求泛型基接口
    /// </summary>
    public interface ISeaweedfsRequest<in T> : ISeaweedfsRequest where T : SeaweedfsResponse
    {

    }
}
