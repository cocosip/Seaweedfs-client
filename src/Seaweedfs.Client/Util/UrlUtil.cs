using Seaweedfs.Client.Extensions;
using System;
using System.Collections.Generic;

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

        /// <summary>获取Url地址的ip地址与端口号,如:127.0.0.1:8080
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string GetUrlAddress(string url)
        {
            var uri = new Uri(url);
            return uri.Authority;
        }

        /// <summary>获取Url地址的基地址
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string GetBaseUrl(string url)
        {
            var uri = new Uri(url);
            return $"{uri.Scheme}://{uri.Authority}";
        }

        /// <summary>获取Query中的参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQuerys(string url)
        {
            var parameters = new Dictionary<string, string>();
            var uri = new Uri(url);
            var queryString = uri.Query.TrimStart('?');
            var queryArray = queryString.Split('&');
            foreach (var queryItem in queryArray)
            {
                var kvArray = queryItem.Split('=');
                if (kvArray.Length == 2 && !kvArray[0].IsNullOrWhiteSpace() && !kvArray[1].IsNullOrWhiteSpace())
                {
                    parameters.Add(kvArray[0], kvArray[1]);
                }
            }
            return parameters;
        }

    }
}
