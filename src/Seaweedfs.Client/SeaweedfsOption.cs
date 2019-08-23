using System.Collections.Generic;

namespace Seaweedfs.Client
{
    /// <summary>Seaweedfs配置信息
    /// </summary>
    public class SeaweedfsOption
    {
        /// <summary>Rest配置
        /// </summary>
        public RestOption RestOption { get; set; }

        /// <summary>Grpc配置
        /// </summary>
        public GrpcOption GrpcOption { get; set; }

        /// <summary>Ctor
        /// </summary>
        public SeaweedfsOption()
        {
            RestOption = new RestOption();
            GrpcOption = new GrpcOption();
        }
    }

    /// <summary>服务器地址
    /// </summary>
    public class ServerAddress
    {
        /// <summary>IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>Ctor
        /// </summary>
        public ServerAddress()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public ServerAddress(string ipAddress, int port)
        {
            IPAddress = ipAddress;
            Port = port;
        }
    }

    /// <summary>Grpc配置
    /// </summary>
    public class GrpcOption
    {
        /// <summary>Grpc的Master连接节点
        /// </summary>
        public List<ServerAddress> GrpcMasters { get; set; } = new List<ServerAddress>();

        /// <summary>同步Grpc Master中Leader的时间间隔,以s为单位
        /// </summary>
        public int SyncGrpcMasterLeaderInterval { get; set; } = 30;

        /// <summary>是否启用GrpcTls认证
        /// </summary>
        public bool EnableTls { get; set; }

        /// <summary>CA证书
        /// </summary>
        public string Ca { get; set; }

        /// <summary>Master证书位置
        /// </summary>
        public string MasterCert { get; set; }

        /// <summary>Master证书Key
        /// </summary>
        public string MasterKey { get; set; }

        /// <summary>Volume证书位置
        /// </summary>
        public string VolumeCert { get; set; }

        /// <summary>Volume证书Key
        /// </summary>
        public string VolumeKey { get; set; }

        /// <summary>Filer证书位置
        /// </summary>
        public string FilerCert { get; set; }

        /// <summary>Filer证书Key
        /// </summary>
        public string FilerKey { get; set; }
    }

    /// <summary>Rest请求配置信息
    /// </summary>
    public class RestOption
    {
        /// <summary>Master服务器地址
        /// </summary>
        public List<ServerAddress> Masters { get; set; } = new List<ServerAddress>();

        /// <summary>架构,默认使用Http架构
        /// </summary>
        public string Scheme { get; set; } = "http";

        /// <summary>同步Master中Leader的时间间隔,以s为单位
        /// </summary>
        public int SyncMasterLeaderInterval { get; set; } = 30;

        /// <summary>是否开启Jwt认证
        /// </summary>
        public bool EnableJwt { get; set; } = false;

        /// <summary>Jwt超时的秒数,以s为单位
        /// </summary>
        public int JwtTimeoutSeconds { get; set; } = 10;

        /// <summary>是否开启读文件Jwt认证
        /// </summary>
        public bool EnableReadJwt { get; set; } = false;

        /// <summary>读文件Jwt超时的秒数,以s为单位
        /// </summary>
        public int ReadJwtTimeoutSeconds { get; set; } = 10;
    }


}
