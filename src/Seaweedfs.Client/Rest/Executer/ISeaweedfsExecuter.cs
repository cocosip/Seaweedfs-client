using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>执行器
    /// </summary>
    public interface ISeaweedfsExecuter
    {

        /// <summary>执行Seaweedfs请求
        /// </summary>
        Task<T> ExecuteAsync<T>(Connection connection, ISeaweedfsRequest<T> request) where T : SeaweedfsResponse;
    }
}
