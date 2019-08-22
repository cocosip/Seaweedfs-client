using System.Collections.Generic;

namespace Seaweedfs.Client
{
    /// <summary>Seaweedfs配置信息
    /// </summary>
    public class SeaweedfsOption
    {
        /// <summary>Master
        /// </summary>
        public List<MasterServer> Masters { get; set; } = new List<MasterServer>();

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

        /// <summary>Master
        /// </summary>
        public class MasterServer
        {
            /// <summary>IP地址
            /// </summary>
            public string IPAddress { get; set; }

            /// <summary>端口号
            /// </summary>
            public int Port { get; set; }

            /// <summary>Ctor
            /// </summary>
            public MasterServer()
            {

            }

            /// <summary>Ctor
            /// </summary>
            public MasterServer(string ipAddress, int port)
            {
                IPAddress = ipAddress;
                Port = port;
            }
        }
    }
}
