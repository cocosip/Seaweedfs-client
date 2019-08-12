using Seaweedfs.Client.Extensions;

namespace Seaweedfs.Client.Rest
{
    /// <summary>分配文件Key请求
    /// </summary>
    public class AssignFileKeyRequest : ISeaweedfsRequest<AssignFileKeyResponse>
    {
        /// <summary>复制机制
        /// </summary>
        public string Replication { get; set; }

        /// <summary>获取数量
        /// </summary>
        public int? Count { get; set; }

        /// <summary>数据中心
        /// </summary>
        public string DataCenter { get; set; }

        /// <summary>有效时间
        /// </summary>
        public string Ttl { get; set; }

        /// <summary>集合
        /// </summary>
        public string Collection { get; set; }

        /// <summary>Ctor
        /// </summary>
        public AssignFileKeyRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public AssignFileKeyRequest(string replication, int? count, string dataCenter, string ttl, string collection)
        {
            Replication = replication;
            Count = count;
            DataCenter = dataCenter;
            Ttl = ttl;
            Collection = collection;
        }

        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/dir/assign", Method.GET);
            if (!Replication.IsNullOrWhiteSpace())
            {
                builder.AddParameter("replication", Replication, ParameterType.QueryString);
            }
            if (Count.HasValue)
            {
                builder.AddParameter("count", Count.Value, ParameterType.QueryString);
            }
            if (!DataCenter.IsNullOrWhiteSpace())
            {
                builder.AddParameter("dataCenter", DataCenter, ParameterType.QueryString);
            }
            if (!Ttl.IsNullOrWhiteSpace())
            {
                builder.AddParameter("ttl", Ttl, ParameterType.QueryString);
            }
            if (!Collection.IsNullOrWhiteSpace())
            {
                builder.AddParameter("collection", Collection, ParameterType.QueryString);
            }

            return builder;
        }
    }
}
