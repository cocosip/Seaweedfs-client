namespace Seaweedfs.Client.Rest
{
    /// <summary>删除集合请求
    /// </summary>
    public class DeleteCollectionRequest : ISeaweedfsRequest<DeleteCollectionResponse>
    {

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
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/col/delete", Method.DELETE);
            builder.AddParameter("collection", Collection, ParameterType.QueryString);
            return builder;
        }
    }
}
