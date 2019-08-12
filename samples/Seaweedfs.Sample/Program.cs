using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Seaweedfs.Client;
using Seaweedfs.Client.Rest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Seaweedfs.Sample
{
    class Program
    {
        static IServiceProvider _provider;
        static ISeaweedfsClient _seaweedfsClient;
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging()
                .AddSeaweedfs("Seaweedfs.xml");
            _provider = services.BuildServiceProvider();
            _provider.ConfigureSeaweedfs();

            _seaweedfsClient = _provider.GetService<ISeaweedfsClient>();

            RunAsync().Wait();
            Console.ReadLine();
        }

        static List<(string, string)> UploadFids = new List<(string, string)>();

        public static async Task RunAsync()
        {
            //上传
            await UploadFiles();
            //下载文件
            DownloadFiles();
        }

        /// <summary>上传文件
        /// </summary>
        public static async Task UploadFiles()
        {
            Console.WriteLine("-------------上传文件测试---------");
            Stopwatch watch = new Stopwatch();
            var dir = new DirectoryInfo(@"D:\Pictures");
            var fileInfos = dir.GetFiles();
            long totalSize = 0;
            watch.Start();
            foreach (var fileInfo in fileInfos)
            {
                var assignFileKeyResponse = await _seaweedfsClient.AssignFileKey();
                var assignFileKey = new AssignFileKey(assignFileKeyResponse.Fid, assignFileKeyResponse.Url);
                var uploadFileResponse = await _seaweedfsClient.UploadFile(assignFileKey, fileInfo.FullName).ConfigureAwait(false);
                Console.WriteLine("IsSuccessful:{0},Fid:{1},ETag:{2},Size:{3}", uploadFileResponse.IsSuccessful, assignFileKeyResponse.Fid, uploadFileResponse.ETag, uploadFileResponse.Size);
                UploadFids.Add((assignFileKeyResponse.Fid, uploadFileResponse.Name));
                totalSize += fileInfo.Length;
            }
            watch.Stop();
            Console.WriteLine("共上传:{0}个文件,总共:{1}Mb,花费:{2},速度:{3} Mb/s", fileInfos.Length, (totalSize / (1024.00 * 1024.00)).ToString("F2"), watch.Elapsed, ((totalSize / (watch.Elapsed.TotalSeconds * 1024.0 * 1024.0))).ToString("F2"));
        }


        /// <summary>下载测试
        /// </summary>
        public static void DownloadFiles()
        {
            Console.WriteLine("-------------下载文件测试---------");
            Stopwatch watch = new Stopwatch();
            var saveDir = @"G:\DownloadTest";
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            IRestClient restClient = new RestClient("http://localhost:8080/");
            watch.Start();
            foreach (var v in UploadFids)
            {
                var url = _seaweedfsClient.GetDownloadUrl(v.Item1);
                var ext = GetPathExtension(v.Item2);
                var savePath = Path.Combine(saveDir, $"{Guid.NewGuid().ToString()}{ext}");
                var request = new RestRequest($"/{v.Item1}");
                var data = restClient.DownloadData(request);
                File.WriteAllBytes(savePath, data);
                Console.WriteLine("下载文件,Fid:{0},Url:{1},保存路径:{2}", v.Item1, url, savePath);
            }
            watch.Stop();
            //获取下载的文件总大小
            var dir = new DirectoryInfo(saveDir);
            var fileInfos = dir.GetFiles();
            long totalSize = 0;
            foreach (var fileInfo in fileInfos)
            {
                totalSize += fileInfo.Length;
            }
            Console.WriteLine("共下载:{0}个文件,总共:{1}Mb,花费:{2},速度:{3} Mb/s", fileInfos.Length, (totalSize / (1024.00 * 1024.00)), watch.Elapsed, (totalSize / (watch.Elapsed.TotalSeconds * 1024.0 * 1024.0)).ToString("F2"));

        }


        /// <summary>获取某个路径中文件的扩展名
        /// </summary>
        public static string GetPathExtension(string path)
        {
            if (path != "" && path.IndexOf('.') >= 0)
            {
                return path.Substring(path.LastIndexOf('.'));
            }
            return "";
        }
    }
}
