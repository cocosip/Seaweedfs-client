namespace Seaweedfs.Client.Rest
{
    /// <summary>查询集群状态请求
    /// </summary>
    public class ClusterStatusRequest : ISeaweedfsRequest<ClusterStatusResponse>
    {

        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/cluster/status", Method.GET);
            return builder;
        }
    }
}
