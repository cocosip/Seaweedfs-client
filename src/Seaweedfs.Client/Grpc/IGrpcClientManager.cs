using System;
using System.Collections.Generic;
using System.Text;
using MasterPb;
using VolumeServerPb;
namespace Seaweedfs.Client.Grpc
{
    /// <summary>Grpc管道管理
    /// </summary>
    public interface IGrpcClientManager
    {
        /// <summary>获取Master Grpc客户端
        /// </summary>
        Seaweed.SeaweedClient GetMasterClient();

        /// <summary>获取Volume Grpc客户端
        /// </summary>
        VolumeServer.VolumeServerClient GetVolumeClient();
    }
}
