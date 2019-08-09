namespace Seaweedfs.Client.Util
{
    /// <summary>Url工具类
    /// </summary>
    public static class UrlUtil
    {
        /// <summary>将架构,ip/host,端口号转换为Url
        /// </summary>
        /// <param name="schema">架构,http/https</param>
        /// <param name="ipAddress">ip地址或者url</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public static string ToUrl(string schema, string ipAddress, int port)
        {
            return $"{schema}://{ipAddress}:{port}";
        }

    }
}
