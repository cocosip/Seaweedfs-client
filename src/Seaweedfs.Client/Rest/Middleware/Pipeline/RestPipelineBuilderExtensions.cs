using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>管道中间件创建扩展
    /// </summary>
    public static class RestPipelineBuilderExtensions
    {
        internal const string InvokeMethodName = "Invoke";
        internal const string InvokeAsyncMethodName = "InvokeAsync";

        /// <summary>使用管道
        /// </summary>
        public static IRestPipelineBuilder UseMiddleware<TMiddleware>(this IRestPipelineBuilder app, params object[] args)
        {
            return app.UseMiddleware(typeof(TMiddleware), args);
        }

        /// <summary>使用管道
        /// </summary>
        /// <returns></returns>
        public static IRestPipelineBuilder UseMiddleware(this IRestPipelineBuilder app, Type middleware, params object[] args)
        {
            return app.Use(next =>
            {
                var methods = middleware.GetMethods(BindingFlags.Instance | BindingFlags.Public);
                var invokeMethods = methods.Where(m =>
                    string.Equals(m.Name, InvokeMethodName, StringComparison.Ordinal)
                    || string.Equals(m.Name, InvokeAsyncMethodName, StringComparison.Ordinal)
                ).ToArray();

                if (invokeMethods.Length > 1)
                {
                    throw new InvalidOperationException();
                }

                if (invokeMethods.Length == 0)
                {
                    throw new InvalidOperationException();
                }

                var methodinfo = invokeMethods[0];
                if (!typeof(Task).IsAssignableFrom(methodinfo.ReturnType))
                {
                    throw new InvalidOperationException();
                }

                var parameters = methodinfo.GetParameters();
                if (parameters.Length == 0 || parameters[0].ParameterType != typeof(RestExecuteContext))
                {
                    throw new InvalidOperationException();
                }

                var ctorArgs = new object[args.Length + 1];
                ctorArgs[0] = next;
                Array.Copy(args, 0, ctorArgs, 1, args.Length);

                var instance = ActivatorUtilities.CreateInstance(app.Provider, middleware, ctorArgs);
                var quickPayExecuteDelegate = (RestExecuteDelegate)methodinfo.CreateDelegate(typeof(RestExecuteDelegate), instance);

                return context =>
                {
                    return quickPayExecuteDelegate(context);
                };
            });
        }

        /// <summary>使用
        /// </summary>
        public static IRestPipelineBuilder Use(this IRestPipelineBuilder app, Func<RestExecuteContext, Func<Task>, Task> middleware)
        {
            return app.Use(next =>
            {
                return context =>
                {
                    Func<Task> simpleNext = () => next(context);
                    return middleware(context, simpleNext);
                };
            });
        }
    }
}
