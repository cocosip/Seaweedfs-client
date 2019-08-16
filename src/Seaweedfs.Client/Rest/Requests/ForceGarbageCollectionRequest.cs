namespace Seaweedfs.Client.Rest
{
    /// <summary>垃圾回收请求
    /// </summary>
    public class ForceGarbageCollectionRequest : BaseSeaweedfsRequest<ForceGarbageCollectionResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/vol/vacuum";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Master;

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
        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.GET);
            if (GarbageThreshold.HasValue)
            {
                builder.AddParameter("garbageThreshold", GarbageThreshold, ParameterType.QueryString);
            }

            return builder;
        }
    }
}
