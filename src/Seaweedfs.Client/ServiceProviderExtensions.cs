using Microsoft.Extensions.DependencyInjection;
using Seaweedfs.Client.Rest;
using System;

namespace Seaweedfs.Client
{
    /// <summary>ServiceProvider扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>配置Seaweedfs
        /// </summary>
        public static IServiceProvider ConfigureSeaweedfs(this IServiceProvider provider)
        {
            //连接管理器
            var connectionManager = provider.GetService<IConnectionManager>();
            connectionManager.Start();

            //Pipeline
            var pipelineBuilder = provider.GetService<IRestPipelineBuilder>();
            pipelineBuilder
                .UseMiddleware<BuildHttpMiddleware>()
                .UseMiddleware<PreAuthenticationMiddleware>() //上传认证
                .UseMiddleware<ExecuterExecuteMiddleware>()
                .UseMiddleware<AssignJwtMiddleware>()
                .UseMiddleware<LookupJwtMiddleware>()
                .UseMiddleware<EndMiddleware>();

            return provider;
        }
    }
}
