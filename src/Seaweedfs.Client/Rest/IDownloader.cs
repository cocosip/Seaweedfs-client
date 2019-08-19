using System;
using System.IO;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{
    /// <summary>文件下载器
    /// </summary>
    public interface IDownloader
    {
        /// <summary>下载文件
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        Task<string> DownloadFileAsync(string fid, string savePath);

        /// <summary>下载文件
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="writer">写入操作</param>
        /// <returns></returns>
        Task DownloadFileAsync(string fid, Action<Stream> writer);
    }
}
