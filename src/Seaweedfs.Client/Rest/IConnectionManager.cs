namespace Seaweedfs.Client.Rest
{
    /// <summary>连接管理器
    /// </summary>
    public interface IConnectionManager
    {

        /// <summary>获取Master连接
        /// </summary>
        Connection GetMasterConnection();

        /// <summary>运行
        /// </summary>
        void Start();

        /// <summary>停止
        /// </summary>
        void Shutdown();
    }
}
