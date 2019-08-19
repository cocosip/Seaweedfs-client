using System;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>创建Http请求数据中间件
    /// </summary>
    public class BuildHttpMiddleware : RestMiddleware
    {
        private readonly RestExecuteDelegate _next;

        /// <summary>Ctor
        /// </summary>
        public BuildHttpMiddleware(IServiceProvider provider, RestExecuteDelegate next) : base(provider)
        {
            _next = next;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task InvokeAsync(RestExecuteContext context)
        {
            try
            {
                var httpBuilder = context.Request.CreateBuilder();
                context.Builder = httpBuilder;
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new BuildHttpError($"构建HttpBuilder出错,{ex.Message}"));
                return;
            }

            await _next.Invoke(context);
        }


    }
}
