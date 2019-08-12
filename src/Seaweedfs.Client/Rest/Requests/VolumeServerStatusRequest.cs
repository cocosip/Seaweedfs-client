namespace Seaweedfs.Client.Rest
{

    /// <summary>获取VolumeServer服务器状态请求
    /// </summary>
    public class VolumeServerStatusRequest : ISeaweedfsRequest<VolumeServerStatusResponse>
    {

        /// <summary>创建HttpBuilder
        /// </summary>

        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/status", Method.GET);
            return builder;
        }
    }
}
