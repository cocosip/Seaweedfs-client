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

            return provider;
        }
    }
}
