namespace Seaweedfs.Client.Rest
{
    /// <summary>删除集合请求
    /// </summary>
    public class DeleteCollectionRequest : BaseSeaweedfsRequest<DeleteCollectionResponse>
    {
        /// <summary>请求资源
        /// </summary>
        public override string Resource { get; set; } = "/col/delete";

        /// <summary>服务器端类型
        /// </summary>
        public override ServerType ServerType { get; set; } = ServerType.Master;

        /// <summary>集合名
        /// </summary>
        public string Collection { get; set; }

        /// <summary>Ctor
        /// </summary>
        public DeleteCollectionRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public DeleteCollectionRequest(string collection)
        {
            Collection = collection;
        }

        /// <summary>创建HttpBuilder
        /// </summary>
        public override HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Resource, Method.DELETE);
            builder.AddParameter("collection", Collection, ParameterType.QueryString);
            return builder;
        }
    }
}
