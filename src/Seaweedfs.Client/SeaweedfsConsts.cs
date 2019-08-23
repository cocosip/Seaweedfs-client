namespace Seaweedfs.Client
{
    /// <summary>常量
    /// </summary>
    public class SeaweedfsConsts
    {

        /// <summary>日志名称
        /// </summary>
        public const string LoggerName = "SeaweedfsLogger";

        /// <summary>任务名
        /// </summary>
        public class ScheduleTaskName
        {
            /// <summary>同步MasterLeader
            /// </summary>
            public const string SyncMasterLeader = "Seaweedfs.SyncMasterLeader";

            /// <summary>Grpc同步MasterLeader
            /// </summary>
            public const string GrpcSyncMasterLeader = "Seaweedfs.GrpcSyncMasterLeader";

            /// <summary>查询超时的Jwt
            /// </summary>
            public const string ScanTimeoutJwt = "Seaweedfs.ScanTimeoutJwt";

            /// <summary>查询超时的ReadJwt
            /// </summary>
            public const string ScanTimeoutReadJwt = "Seaweedfs.ScanTimeoutReadJwt";
        }
    }
}
