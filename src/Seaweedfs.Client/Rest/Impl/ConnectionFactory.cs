using Seaweedfs.Client.Extensions;
using System;

namespace Seaweedfs.Client.Rest
{
    /// <summary>连接工厂
    /// </summary>
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IServiceProvider _provider;

        /// <summary>Ctor
        /// </summary>
        public ConnectionFactory(IServiceProvider provider)
        {
            _provider = provider;
        }



        /// <summary>创建连接对象
        /// </summary>
        /// <param name="connectionAddress"></param>
        /// <param name="connectionType"></param>
        /// <returns></returns>
        public Connection CreateConnection(ConnectionAddress connectionAddress, ConnectionType connectionType)
        {
            return _provider.CreateInstance<Connection>(connectionAddress, connectionType);
        }

    }
}
