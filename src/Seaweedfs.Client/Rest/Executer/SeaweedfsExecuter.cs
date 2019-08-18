using Microsoft.Extensions.Logging;
using RestSharp;
using Seaweedfs.Client.Serializing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>执行器
    /// </summary>
    public class SeaweedfsExecuter : ISeaweedfsExecuter
    {
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IRestExecuteContextFactory _restExecuteContextFactory;
        private readonly IRestPipelineBuilder _restPipelineBuilder;

        /// <summary>Ctor
        /// </summary>
        public SeaweedfsExecuter(ILoggerFactory loggerFactory, IJsonSerializer jsonSerializer, IRestExecuteContextFactory restExecuteContextFactory, IRestPipelineBuilder restPipelineBuilder)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
            _jsonSerializer = jsonSerializer;
            _restExecuteContextFactory = restExecuteContextFactory;
            _restPipelineBuilder = restPipelineBuilder;
        }


        ///// <summary>执行Seaweedfs请求
        ///// </summary>
        //public async Task<T> ExecuteAsync<T>(Connection connection, ISeaweedfsRequest<T> request) where T : SeaweedfsResponse, new()
        //{
        //    var builder = request.CreateBuilder();
        //    IRestRequest restRequest = builder.BuildRequest();
        //    var response = await connection.ExecuteAsync(restRequest);
        //    var result = new T()
        //    {
        //        IsSuccessful = response.IsSuccessful,
        //        StatusCode = response.StatusCode,
        //        ErrorException = response.ErrorException,
        //        ErrorMessage = response.ErrorMessage
        //    };

        //    if (response.IsSuccessful)
        //    {
        //        try
        //        {
        //            var t = _jsonSerializer.Deserialize<T>(response.Content);
        //            t.IsSuccessful = result.IsSuccessful;
        //            t.StatusCode = result.StatusCode;
        //            return t;
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "执行Seaweedfs请求,对返回值反序列化失败,{0}", ex.Message);
        //            result.IsSuccessful = false;
        //            result.ErrorException = ex;
        //            result.ErrorMessage = ex.Message;
        //        }
        //    }

        //    return result;
        //}

        /// <summary>执行器执行
        /// </summary>
        public async Task<T> ExecuteAsync<T>(Connection connection, ISeaweedfsRequest<T> request) where T : SeaweedfsResponse, new()
        {
            try
            {
                var firstDelegate = _restPipelineBuilder.Build();

                var context = _restExecuteContextFactory.CreateContext<T>(request);
                await firstDelegate(context);
                if (context.IsError)
                {
                    var error = context.Errors.FirstOrDefault();
                    throw new Exception(error.Message);
                }

                if (context.Response != null)
                {
                    return context.Response as T;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"支付ExecuteAsync出错,{ex.Message}");
                throw ex;
            }
        }
    }
}
