namespace Seaweedfs.Client.Rest
{
    /// <summary>连接工厂
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>创建连接对象
        /// </summary>
        /// <param name="connectionAddress"></param>
        /// <param name="connectionType"></param>
        /// <returns></returns>
        Connection CreateConnection(ConnectionAddress connectionAddress, ConnectionType connectionType);
    }
}
