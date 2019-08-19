namespace Seaweedfs.Client.Rest
{
    /// <summary>RestExecuteContext上下文工厂
    /// </summary>
    public class RestExecuteContextFactory : IRestExecuteContextFactory
    {
        /// <summary>创建上下文
        /// </summary>
        public RestExecuteContext CreateContext<T>(ISeaweedfsRequest<T> request) where T : SeaweedfsResponse, new()
        {
            var context = new RestExecuteContext()
            {
                Request = request,
                ServerType = request.ServerType,
                AssignServer = request.AssignServer,
                Response = new T()
            };
            return context;
        }
    }
}
