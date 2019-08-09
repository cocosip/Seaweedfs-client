namespace Seaweedfs.Client.Rest
{
    /// <summary>Seaweedfs请求基接口
    /// </summary>
    public interface ISeaweedfsRequest
    {
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
