namespace Seaweedfs.Client.Rest
{

    /// <summary>获取VolumeServer服务器状态请求
    /// </summary>
    public class VolumeServerStatusRequest : BaseSeaweedfsRequest<VolumeServerStatusResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/status";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Volume;

        /// <summary>创建HttpBuilder
        /// </summary>

        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.GET);
            return builder;
        }
    }
}
