namespace Seaweedfs.Client.Rest
{
    /// <summary>RestExecuteContext上下文工厂
    /// </summary>
    public class RestExecuteContextFactory : IRestExecuteContextFactory
    {
        /// <summary>创建上下文
        /// </summary>
        public RestExecuteContext CreateContext<T>(ISeaweedfsRequest<T> request) where T : SeaweedfsResponse
        {
            var context = new RestExecuteContext()
            {
                Request = request,
                ServerType = request.ServerType,
                Response = default(T)
            };
            return context;
        }
    }
}
