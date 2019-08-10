namespace Seaweedfs.Client.Rest
{
    /// <summary>垃圾回收请求
    /// </summary>
    public class ForceGarbageCollectionRequest : ISeaweedfsRequest<ForceGarbageCollectionResponse>
    {
        /// <summary>阈值
        /// </summary>
        public decimal? GarbageThreshold { get; set; }


        /// <summary>Ctor
        /// </summary>
        public ForceGarbageCollectionRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public ForceGarbageCollectionRequest(decimal? garbageThreshold)
        {
            GarbageThreshold = garbageThreshold;
        }


        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/vol/vacuum", Method.GET);
            if (GarbageThreshold.HasValue)
            {
                builder.AddParameter("GarbageThreshold", GarbageThreshold, ParameterType.QueryString);
            }

            return builder;
        }
    }
}
