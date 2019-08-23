using System;
using System.Linq;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>查询时获取Jwt中间件
    /// </summary>
    public class LookupJwtMiddleware : RestMiddleware
    {
        private readonly RestExecuteDelegate _next;
        private readonly SeaweedfsOption _option;
        private readonly IJwtManager _jwtManager;

        /// <summary>Ctor
        /// </summary>
        public LookupJwtMiddleware(IServiceProvider provider, RestExecuteDelegate next, SeaweedfsOption option, IJwtManager jwtManager) : base(provider)
        {
            _next = next;
            _option = option;
            _jwtManager = jwtManager;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task InvokeAsync(RestExecuteContext context)
        {
            if (!_option.RestOption.EnableJwt)
            {
                await _next.Invoke(context);
                return;
            }

            try
            {
                if (context.Response.GetType() == typeof(LookupResponse))
                {
                    var lookupRequest = (LookupRequest)context.Request;

                    var authentication = context.Response.Headers.FirstOrDefault(x => x.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase));
                    if (lookupRequest.IsRead.HasValue && lookupRequest.IsRead.Value)
                    {
                        if (_option.RestOption.EnableReadJwt)
                        {
                            //添加到Read Jwt管理器
                            _jwtManager.AddReadJwt(context.Request.Fid, authentication.Value.ToString());
                        }
                    }
                    else
                    {
                        //添加到token管理器
                        _jwtManager.AddLookupJwt(context.Request.Fid, authentication.Value.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                SetPipelineError(context, new LookupJwtError($"Lookup Jwt出错,{ex.Message}"));
                return;
            }
            await _next.Invoke(context);
        }
    }
}
