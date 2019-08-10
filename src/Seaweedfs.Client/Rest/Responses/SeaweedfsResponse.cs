using System.Net;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Seaweedfs请求响应基类
    /// </summary>
    public abstract class SeaweedfsResponse
    {
        /// <summary>是否执行成功
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>Http状态
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}
