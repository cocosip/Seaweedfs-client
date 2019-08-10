﻿using Seaweedfs.Client.Extensions;
using System;
using System.IO;
namespace Seaweedfs.Client.Rest
{
    /// <summary>上传文件请求
    /// </summary>
    public class UploadFileRequest : ISeaweedfsRequest<UploadFileResponse>
    {
        /// <summary>文件Id
        /// </summary>
        public string Fid { get; set; }

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
        public UploadFileRequest()
        {

        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="filePath">本地文件路径</param>
        public UploadFileRequest(string fid, string filePath)
        {
            Fid = fid;
            FilePath = filePath;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="fileBytes">文件二进制</param>
        /// <param name="fileName">文件名</param>
        public UploadFileRequest(string fid, byte[] fileBytes, string fileName)
        {
            Fid = fid;
            FileBytes = fileBytes;
            FileName = fileName;
        }

        /// <summary>Ctor
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="writer">文件流写入</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentLength">文件长度</param>
        public UploadFileRequest(string fid, Action<Stream> writer, string fileName, long contentLength)
        {
            Fid = fid;
            Writer = writer;
            FileName = fileName;
            ContentLength = contentLength;
        }



        /// <summary>创建HttpBuilder
        /// </summary>
        public HttpBuilder CreateBuilder()
        {
            var builder = new HttpBuilder(Fid, Method.POST);
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
