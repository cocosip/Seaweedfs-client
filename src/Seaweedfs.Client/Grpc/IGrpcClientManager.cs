using MasterPb;
namespace Seaweedfs.Client.Grpc
{
    /// <summary>Grpc客户端管理
    /// </summary>
    public interface IGrpcClientManager
    {
        /// <summary>获取Master Grpc客户端
        /// </summary>
        Seaweed.SeaweedClient GetMasterClient();

        /// <summary>运行
        /// </summary>
        void Start();

        /// <summary>停止
        /// </summary>
        void Shutdown();
    }
}
