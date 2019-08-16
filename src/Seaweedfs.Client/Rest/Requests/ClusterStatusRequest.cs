namespace Seaweedfs.Client.Rest
{
    /// <summary>查询集群状态请求
    /// </summary>
    public class ClusterStatusRequest : BaseSeaweedfsRequest<ClusterStatusResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/cluster/status";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Master;

        /// <summary>创建HttpBuilder
        /// </summary>
        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.GET);
            return builder;
        }
    }
}
