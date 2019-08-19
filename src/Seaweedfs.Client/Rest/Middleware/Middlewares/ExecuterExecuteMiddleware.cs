using Microsoft.Extensions.Logging;
using RestSharp;
using Seaweedfs.Client.Extensions;
using Seaweedfs.Client.Serializing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>执行器
    /// </summary>
    public class ExecuterExecuteMiddleware : RestMiddleware
    {
        private readonly RestExecuteDelegate _next;
        private readonly IConnectionManager _connectionManager;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>Ctor
        /// </summary>
        public ExecuterExecuteMiddleware(IServiceProvider provider, RestExecuteDelegate next, IConnectionManager connectionManager, IJsonSerializer jsonSerializer) : base(provider)
        {
            _next = next;
            _connectionManager = connectionManager;
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>Invoke
        /// </summary>
        public async Task InvokeAsync(RestExecuteContext context)
        {
            try
            {
                //获取连接
                var connection = GetConnection(context);
                if (connection == null)
                {
                    Logger.LogError("无法根据请求的相关数据获取连接Connection!");
                    SetPipelineError(context, new ExecuteError("无法根据请求的相关数据获取连接Connection!"));
                    return;
                }
                IRestRequest restRequest = context.Builder.BuildRequest();
                var restResponse = await connection.ExecuteAsync(restRequest);
                Logger.LogDebug("Execute执行返回结果为:{0}", restResponse.Content);
                //设置response返回的状态,头部Header等
                if (!restResponse.IsSuccessful)
                {
                    SetPipelineError(context, new ExecuteError("RestResponse返回数据不正确!"));
                    return;
                }
                context.Response = _jsonSerializer.Deserialize(restResponse.Content, context.Response.GetType()) as SeaweedfsResponse;
                context.Response.IsSuccessful = restResponse.IsSuccessful;
                context.Response.ErrorException = restResponse.ErrorException;
                context.Response.ErrorMessage = restResponse.ErrorMessage;

                foreach (var header in restResponse.Headers)
                {
                    context.Response.Headers.Add(header.TransferToParameter());
                }
            }
            catch (Exception ex)
            {
                SetPipelineError(context, new ExecuteError($"调用Execute出错,{ex.Message}"));
                return;
            }

            await _next.Invoke(context);
        }


        private Connection GetConnection(RestExecuteContext context)
        {
            Connection connection = null;
            if (context.ServerType == ServerType.Master)
            {
                connection = _connectionManager.GetMasterConnection();
            }
            else if (context.ServerType == ServerType.Volume)
            {
                //根据指定的服务器地址获取连接
                if (!context.Request.AssignServer.IsNullOrWhiteSpace())
                {
                    connection = _connectionManager.GetVolumeConnectionByUrl(context.AssignServer);
                }
                else if (!context.Request.Fid.IsNullOrWhiteSpace())
                {
                    connection = _connectionManager.GetVolumeConnectionByVolumeIdOrFid(context.Request.Fid);
                }
            }
            return connection;
        }

    }
}
