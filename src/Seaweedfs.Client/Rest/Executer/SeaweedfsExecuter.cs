using Microsoft.Extensions.Logging;
using RestSharp;
using Seaweedfs.Client.Serializing;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>执行器
    /// </summary>
    public class SeaweedfsExecuter : ISeaweedfsExecuter
    {
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>Ctor
        /// </summary>
        public SeaweedfsExecuter(ILoggerFactory loggerFactory, IJsonSerializer jsonSerializer)
        {
            _logger = loggerFactory.CreateLogger(SeaweedfsConsts.LoggerName);
            _jsonSerializer = jsonSerializer;
        }


        /// <summary>执行Seaweedfs请求
        /// </summary>
        public async Task<T> ExecuteAsync<T>(Connection connection, ISeaweedfsRequest<T> request) where T : SeaweedfsResponse
        {
            var builder = request.CreateBuilder();
            IRestRequest restRequest = builder.BuildRequest();
            var response = await connection.Client.ExecuteTaskAsync(restRequest);
            var t = _jsonSerializer.Deserialize<T>(response.Content);
            return t;
        }
    }
}
