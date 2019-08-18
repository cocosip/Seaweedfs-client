using System;

namespace Seaweedfs.Client.Rest
{
    /// <summary>Rest接口管道构建器
    /// </summary>
    public interface IRestPipelineBuilder
    {
        /// <summary>Provider
        /// </summary>
        IServiceProvider Provider { get; }

        /// <summary>Use
        /// </summary>
        IRestPipelineBuilder Use(Func<RestExecuteDelegate, RestExecuteDelegate> middleware);

        /// <summary>Build
        /// </summary>
        RestExecuteDelegate Build();
    }
}
