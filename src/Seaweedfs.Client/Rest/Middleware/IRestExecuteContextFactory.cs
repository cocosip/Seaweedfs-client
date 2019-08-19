namespace Seaweedfs.Client.Rest
{
    /// <summary>RestExecuteContext上下文工厂
    /// </summary>
    public interface IRestExecuteContextFactory
    {
        /// <summary>创建上下文
        /// </summary>
        RestExecuteContext CreateContext<T>(ISeaweedfsRequest<T> request) where T : SeaweedfsResponse, new();
    }
}
