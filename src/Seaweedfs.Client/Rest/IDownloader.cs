using System;
using System.IO;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>文件下载器
    /// </summary>
    public interface IDownloader
    {
        /// <summary>异步下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="savePath">文件保存路径</param>
        /// <returns></returns>
        Task<string> DownloadFileAsync(string url, string savePath);

        /// <summary>异步下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="writer">流文件操作</param>
        /// <returns></returns>
        Task DownloadFileAsync(string url, Action<Stream> writer);
    }
}
