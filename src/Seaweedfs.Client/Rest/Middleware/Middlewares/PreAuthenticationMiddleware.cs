using System;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>上传,删除文件认证中间件
    /// </summary>
    public class PreAuthenticationMiddleware : RestMiddleware
    {
        private readonly RestExecuteDelegate _next;
        private readonly SeaweedfsOption _option;
        private readonly IJwtManager _jwtManager;
        /// <summary>Ctor
        /// </summary>
        public PreAuthenticationMiddleware(IServiceProvider provider, RestExecuteDelegate next, SeaweedfsOption option, IJwtManager jwtManager) : base(provider)
        {
            _next = next;
            _option = option;
            _jwtManager = jwtManager;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task InvokeAsync(RestExecuteContext context)
        {
            if (!_option.EnableJwt)
            {
                await _next.Invoke(context);
                return;
            }

            try
            {
                //上传,删除文件之前获取Jwt
                if (IsPreAuthRequestAuth(context.Request.GetType()))
                {
                    //Assign Jwt
                    var jwt = _jwtManager.GetAssignJwt(context.Request.Fid);

                    //添加认证头部
                    context.Builder.AddParameter("Authorization", jwt, ParameterType.HttpHeader);

                }

            }
            catch (Exception ex)
            {
                SetPipelineError(context, new AssignJwtError($"UploadAuthentication出错,{ex.Message}"));
                return;
            }

            await _next.Invoke(context);

        }


        private bool IsPreAuthRequestAuth(Type type)
        {
            return type == typeof(UploadFileRequest) || type == typeof(DeleteFileRequest);
        }


    }
}
