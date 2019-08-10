using Seaweedfs.Client.Extensions;
using System;
using System.IO;

namespace Seaweedfs.Client.Rest
{

    /// <summary>直接上传文件请求
    /// </summary>
    public class UploadFileDirectlyRequest : ISeaweedfsRequest<UploadFileDirectlyResponse>
    {
        /// <summary>文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>文件二进制
        /// </summary>
        public byte[] FileBytes { get; set; }

        /// <summary>流文件操作
        /// </summary>
        public Action<Stream> Writer { get; set; }

        /// <summary>ContentLength
        /// </summary>
        public long? ContentLength { get; set; }

        /// <summary>Ctor
        /// </summary>
        public UploadFileDirectlyRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="filePath">本地文件路径</param>
        public UploadFileDirectlyRequest(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="fileBytes">文件二进制</param>
        /// <param name="fileName">文件名</param>
        public UploadFileDirectlyRequest(byte[] fileBytes, string fileName)
        {
            FileBytes = fileBytes;
            FileName = fileName;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="writer">文件流写入</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentLength">文件长度</param>
        public UploadFileDirectlyRequest(Action<Stream> writer, string fileName, long contentLength)
        {
            Writer = writer;
            FileName = fileName;
            ContentLength = contentLength;
        }


        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder("/submit", Method.POST);
            if (!FilePath.IsNullOrWhiteSpace())
            {
                builder.AddFile("file", FilePath);
            }
            if (FileBytes != null && FileBytes.Length > 0)
            {
                builder.AddFile("file", FileBytes, FileName);
            }
            if (Writer != null)
            {
                builder.AddFile("file", Writer, FileName, ContentLength.Value);
            }
            return builder;
        }
    }
}
