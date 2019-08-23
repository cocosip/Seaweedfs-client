using Microsoft.Extensions.DependencyInjection;
using Seaweedfs.Client.Grpc;
using Seaweedfs.Client.Rest;
using Seaweedfs.Client.Scheduling;
using Seaweedfs.Client.Serializing;
using System;
using System.Linq;

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
                .AddSingleton<IJwtManager, JwtManager>()
                .AddSingleton<SeaweedfsOption>(option)
                .AddSingleton<IRestExecuteContextFactory, RestExecuteContextFactory>()
                .AddSingleton<IRestPipelineBuilder, RestPipelineBuilder>()
                .AddSingleton<IDownloader, DefaultDownloader>()
                .AddTransient<ISeaweedfsExecuter, SeaweedfsExecuter>()
                .AddTransient<ISeaweedfsClient, SeaweedfsClient>()
                .AddTransient<ISeaweedfsGrpcClient, SeaweedfsGrpcClient>()
                .AddSingleton<IGrpcClientManager, GrpcClientManager>()
                .RegisterPipeline();
            return services;
        }


        //注册Pipeline
        private static IServiceCollection RegisterPipeline(this IServiceCollection services)
        {
            //查询出全部的中间件
            var middlewareTypies = typeof(SeaweedfsConsts).Assembly.GetTypes().Where(x => typeof(RestMiddleware).IsAssignableFrom(x) && x != typeof(RestMiddleware));
            foreach (var middlewareType in middlewareTypies)
            {
                services.AddTransient(middlewareType);
            }
            return services;
        }

    }
}
