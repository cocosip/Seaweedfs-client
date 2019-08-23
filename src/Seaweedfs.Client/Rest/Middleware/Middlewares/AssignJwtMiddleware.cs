using Seaweedfs.Client.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Assign Jwt分配中间件
    /// </summary>
    public class AssignJwtMiddleware : RestMiddleware
    {
        private readonly RestExecuteDelegate _next;
        private readonly SeaweedfsOption _option;
        private readonly IJwtManager _jwtManager;

        /// <summary>Ctor
        /// </summary>
        public AssignJwtMiddleware(IServiceProvider provider, RestExecuteDelegate next, SeaweedfsOption option, IJwtManager jwtManager) : base(provider)
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
                //开启Jwt认证,需要在发起了AssignFileKey请求之后,将数据保存下来
                if (context.Response.GetType() == typeof(AssignFileKeyResponse))
                {
                    var fid = ((AssignFileKeyResponse)context.Response).Fid;
                    var authentication = context.Response.Headers.FirstOrDefault(x => x.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase));
                    //添加到token管理器
                    _jwtManager.AddAssignJwt(fid, authentication.Value.ToString());
                }

            }
            catch (Exception ex)
            {
                SetPipelineError(context, new AssignJwtError($"AssignJwt出错,{ex.Message}"));
                return;
            }

            await _next.Invoke(context);

        }
    }
}
