namespace Seaweedfs.Client.Rest
{
    /// <summary>连接管理器
    /// </summary>
    public interface IConnectionManager
    {

        /// <summary>获取Master连接
        /// </summary>
        Connection GetMasterConnection();

        /// <summary>根据AssignFileKeyResponse获取Volume连接
        /// </summary>
        Connection GetVolumeConnectionByAssignFileKey(AssignFileKey assignFileKey);

        /// <summary>根据VolumeId(可以是Fid)获取Volume连接
        /// </summary>
        Connection GetVolumeConnectionByVolumeIdOrFid(string volumeIdOrFid);

        /// <summary>根据Url获取Volume连接
        /// </summary>
        Connection GetVolumeConnectionByUrl(string url);

        /// <summary>运行
        /// </summary>
        void Start();

        /// <summary>停止
        /// </summary>
        void Shutdown();
    }
}
