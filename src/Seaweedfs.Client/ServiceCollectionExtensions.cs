using Microsoft.Extensions.DependencyInjection;
using Seaweedfs.Client.Rest;
using Seaweedfs.Client.Scheduling;
using Seaweedfs.Client.Serializing;
using System;

namespace Seaweedfs.Client
{
    /// <summary>ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>添加Seaweedfs
        /// </summary>
        public static IServiceCollection AddSeaweedfs(this IServiceCollection services, Action<SeaweedfsOption> configure = null)
        {
            var option = new SeaweedfsOption();
            configure?.Invoke(option);
            return services.AddSeaweedfs(option);
        }

        /// <summary>添加Seaweedfs
        /// </summary>
        public static IServiceCollection AddSeaweedfs(this IServiceCollection services, string file)
        {
            var option = SeaweedfsOptionHelper.GetSeaweedfsOption(file);
            return services.AddSeaweedfs(option);
        }


        /// <summary>添加Seaweedfs
        /// </summary>
        public static IServiceCollection AddSeaweedfs(this IServiceCollection services, SeaweedfsOption option)
        {
            services
                .AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>()
                .AddSingleton<IScheduleService, ScheduleService>()
                .AddSingleton<IConnectionFactory, ConnectionFactory>()
                .AddSingleton<IConnectionManager, ConnectionManager>()
                .AddSingleton<SeaweedfsOption>(option)
                .AddTransient<ISeaweedfsExecuter, SeaweedfsExecuter>()
                .AddTransient<ISeaweedfsClient, SeaweedfsClient>();
            return services;
        }

    }
}
