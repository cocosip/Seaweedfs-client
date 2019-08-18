using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Rest接口管道构建器
    /// </summary>
    public class RestPipelineBuilder : IRestPipelineBuilder
    {
        private readonly IList<Func<RestExecuteDelegate, RestExecuteDelegate>> _middlewares;

        /// <summary>Provider
        /// </summary>
        public IServiceProvider Provider { get; }

        /// <summary>Ctor
        /// </summary>
        public RestPipelineBuilder(IServiceProvider provider)
        {
            Provider = provider;
            _middlewares = new List<Func<RestExecuteDelegate, RestExecuteDelegate>>();
        }


        /// <summary>Use
        /// </summary>
        public IRestPipelineBuilder Use(Func<RestExecuteDelegate, RestExecuteDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }

        /// <summary>Build
        /// </summary>
        public RestExecuteDelegate Build()
        {
            RestExecuteDelegate app = context =>
            {
                context.Response = null;
                return Task.CompletedTask;
            };
            foreach (var component in _middlewares.Reverse())
            {
                app = component(app);
            }
            return app;
        }
    }
}
