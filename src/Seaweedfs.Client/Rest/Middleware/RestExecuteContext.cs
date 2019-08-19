using System.Collections.Generic;
using System.Linq;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Rest接口执行上下文
    /// </summary>
    public class RestExecuteContext
    {
        /// <summary>服务器端类型
        /// </summary>
        public ServerType ServerType { get; set; }

        /// <summary>指定的服务器端地址
        /// </summary>
        public string AssignServer { get; set; }

        /// <summary>HttpBuilder
        /// </summary>
        public HttpBuilder Builder { get; set; }

        /// <summary>请求
        /// </summary>
        public ISeaweedfsRequest Request { get; set; }

        /// <summary>响应
        /// </summary>
        public SeaweedfsResponse Response { get; set; }

        /// <summary>错误信息
        /// </summary>
        public List<Error> Errors { get; } = new List<Error>();

        /// <summary>是否有错误
        /// </summary>
        public bool IsError => Errors.Any();
    }
}
