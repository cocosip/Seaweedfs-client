using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Rest接口Middleware基类
    /// </summary>
    public abstract class RestMiddleware
    {
        /// <summary>中间件名称
        /// </summary>
        public string MiddlewareName => GetType().Name;

        /// <summary>Provider
        /// </summary>
        protected IServiceProvider Provider { get; private set; }

        /// <summary>Logger
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>Ctor
        /// </summary>
        public RestMiddleware(IServiceProvider provider)
        {
            Provider = provider;
            Logger = provider.GetService<ILoggerFactory>().CreateLogger(SeaweedfsConsts.LoggerName);
        }

        /// <summary>设置错误信息
        /// </summary>
        public void SetPipelineError(RestExecuteContext context, List<Error> errors)
        {
            foreach (var error in errors)
            {
                SetPipelineError(context, error);
            }
        }

        /// <summary>设置错误信息
        /// </summary>
        public void SetPipelineError(RestExecuteContext context, Error error)
        {
            if (context.Request == null)
            {
                Logger.LogError("执行Seaweedfs请求出错,RestExecuteContext中的Request为空.{0}", error.Message);
            }
            context?.Errors.Add(error);
        }
    }
}
