using Seaweedfs.Client.Extensions;

namespace Seaweedfs.Client.Rest
{
    /// <summary>分配文件Key请求
    /// </summary>
    public class AssignFileKeyRequest : ISeaweedfsRequest<AssignFileKeyResponse>
    {
        /// <summary>复制机制
        /// </summary>
        public string Replication { get; }

        /// <summary>获取数量
        /// </summary>
        public int? Count { get; }

        /// <summary>数据中心
        /// </summary>
        public string DataCenter { get; }

        /// <summary>有效时间
        /// </summary>
        public string Ttl { get; }

        /// <summary>集合
        /// </summary>
        public string Collection { get; }

        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/dir/assign", Method.GET);
            if (!Replication.IsNullOrWhiteSpace())
            {
                builder.AddParameter("Replication", Replication, ParameterType.QueryString);
            }
            if (Count.HasValue)
            {
                builder.AddParameter("Count", Count.Value, ParameterType.QueryString);
            }
            if (!DataCenter.IsNullOrWhiteSpace())
            {
                builder.AddParameter("DataCenter", DataCenter, ParameterType.QueryString);
            }
            if (!Ttl.IsNullOrWhiteSpace())
            {
                builder.AddParameter("Ttl", Ttl, ParameterType.QueryString);
            }
            if (!Collection.IsNullOrWhiteSpace())
            {
                builder.AddParameter("Collection", Collection, ParameterType.QueryString);
            }

            return builder;
        }
    }
}
