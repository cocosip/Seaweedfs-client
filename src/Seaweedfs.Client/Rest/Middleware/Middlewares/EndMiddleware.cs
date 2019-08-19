using System;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>结尾中间件
    /// </summary>
    public class EndMiddleware : RestMiddleware
    {
        private readonly RestExecuteDelegate _next;

        /// <summary>Ctor
        /// </summary>
        public EndMiddleware(IServiceProvider provider, RestExecuteDelegate next) : base(provider)
        {
            _next = next;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task InvokeAsync(RestExecuteContext context)
        {
            await Task.CompletedTask;
            return;
        }
    }
}
