namespace Seaweedfs.Client.Grpc.Content
{
    /// <summary>获取Master信息
    /// </summary>
    public class MasterConfiguration
    {
        /// <summary>监控地址
        /// </summary>
        public string MetricsAddress { get; set; }

        /// <summary>时间间隔
        /// </summary>
        public int MetricsIntervalSeconds { get; set; }

        /// <summary>Ctor
        /// </summary>
        public MasterConfiguration()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public MasterConfiguration(string metricsAddress, int metricsIntervalSeconds)
        {
            MetricsAddress = metricsAddress;
            MetricsIntervalSeconds = metricsIntervalSeconds;
        }
    }
}
