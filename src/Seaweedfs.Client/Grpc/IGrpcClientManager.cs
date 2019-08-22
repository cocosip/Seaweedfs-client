using System;
using System.Collections.Generic;
using System.Text;
using MasterPb;
namespace Seaweedfs.Client.Grpc
{
    /// <summary>Grpc管道管理
    /// </summary>
    public interface IGrpcClientManager
    {
        /// <summary>获取Master Grpc客户端
        /// </summary>
        Seaweed.SeaweedClient GetMasterClient();
    }
}
