namespace Seaweedfs.Client.Rest
{
    /// <summary>系统状态请求
    /// </summary>
    public class SystemStatusRequest : ISeaweedfsRequest<SystemStatusResponse>
    {

        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/dir/status", Method.GET);
            return builder;
        }
    }
}
