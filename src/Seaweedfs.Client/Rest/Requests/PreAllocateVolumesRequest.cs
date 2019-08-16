using Seaweedfs.Client.Extensions;

namespace Seaweedfs.Client.Rest
{
    /// <summary>预分配Volumes请求
    /// </summary>
    public class PreAllocateVolumesRequest : BaseSeaweedfsRequest<PreAllocateVolumesResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/vol/grow";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Master;

        /// <summary>数量
        /// </summary>
        public int Count { get; set; } = 1;

        /// <summary>数据中心
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>Replication
        /// </summary>
        public string Replication { get; set; }

        /// <summary>时效
        /// </summary>
        public string Ttl { get; set; }

        /// <summary>Collection
        /// </summary>
        public string Collection { get; set; }


        /// <summary>Ctor
        /// </summary>
        public PreAllocateVolumesRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public PreAllocateVolumesRequest(int count)
        {
            Count = count;
        }

        /// <summary>Ctor
        /// </summary>
        public PreAllocateVolumesRequest(string dataCenter)
        {
            DataCenter = dataCenter;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="replication">复制策略</param>
        /// <param name="count">数量</param>
        /// <param name="dataCenter">数据中心</param>
        /// <param name="ttl">时效</param>
        public PreAllocateVolumesRequest(string replication, int count, string dataCenter, string ttl)
        {
            Replication = replication;
            Count = count;
            DataCenter = dataCenter;
            Ttl = ttl;
        }

        /// <summary>Ctor
        /// </summary>
        public PreAllocateVolumesRequest(int count, string dataCenter, string replication, string ttl, string collection)
        {
            Count = count;
            DataCenter = dataCenter;
            Replication = replication;
            Ttl = ttl;
            Collection = collection;
        }


        /// <summary>创建HttpBuilder
        /// </summary>
        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.GET);
            builder.AddParameter("count", Count, ParameterType.QueryString);
            if (!DataCenter.IsNullOrWhiteSpace())
            {
                builder.AddParameter("dataCenter", DataCenter, ParameterType.QueryString);
            }
            if (!Replication.IsNullOrWhiteSpace())
            {
                builder.AddParameter("replication", Replication, ParameterType.QueryString);
            }
            if (!Collection.IsNullOrWhiteSpace())
            {
                builder.AddParameter("collection", Collection, ParameterType.QueryString);
            }
            if (!Ttl.IsNullOrWhiteSpace())
            {
                builder.AddParameter("ttl", Ttl, ParameterType.QueryString);
            }
            return builder;
        }
    }
}
