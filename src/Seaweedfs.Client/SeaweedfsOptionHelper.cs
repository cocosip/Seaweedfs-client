using System;
using System.IO;
using System.Text;
using System.Xml;
using static Seaweedfs.Client.SeaweedfsOption;

namespace Seaweedfs.Client
{

    /// <summary>SeaweedfsOption配置辅助类
    /// </summary>
    public static class SeaweedfsOptionHelper
    {

        /// <summary>从配置文件加载配置信息
        /// </summary>
        /// <param name="file">配置文件位置</param>
        /// <returns>配置选项<see cref="Seaweedfs.Client.SeaweedfsOption" /></returns>
        public static SeaweedfsOption GetSeaweedfsOption(string file)
        {
            if (!File.Exists(file))
            {
                throw new ArgumentException("配置文件不存在");
            }
            var option = new SeaweedfsOption();
            var doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true //忽略文档里面的注释
            };
            XmlReader reader = XmlReader.Create(file, settings);
            doc.Load(reader);
            XmlNode root = doc.SelectSingleNode("Seaweedfs");

            //Rest节点
            var restNode = root.SelectSingleNode("RestOption");
            var masterNode = restNode.SelectSingleNode("Masters");
            var serverAddressNodes = masterNode.SelectNodes("ServerAddress");
            foreach (XmlNode serverAddressNode in serverAddressNodes)
            {
                var ipAddress = serverAddressNode.SelectSingleNode("IPAddress").InnerText;
                var port = int.Parse(serverAddressNode.SelectSingleNode("Port").InnerText);
                option.RestOption.Masters.Add(new ServerAddress(ipAddress, port));
            }
            //架构,http/https
            option.RestOption.Scheme = restNode.SelectSingleNode("Scheme").InnerText;
            //Master同步时间间隔
            option.RestOption.SyncMasterLeaderInterval = int.Parse(restNode.SelectSingleNode("SyncMasterLeaderInterval").InnerText);
            //是否启用jwt
            option.RestOption.EnableJwt = bool.Parse(restNode.SelectSingleNode("EnableJwt").InnerText);
            //Jwt超时时间
            option.RestOption.JwtTimeoutSeconds = int.Parse(restNode.SelectSingleNode("JwtTimeoutSeconds").InnerText);
            //是否启用读取文件jwt
            option.RestOption.EnableReadJwt = bool.Parse(restNode.SelectSingleNode("EnableReadJwt").InnerText);
            //读文件Jwt
            option.RestOption.ReadJwtTimeoutSeconds = int.Parse(restNode.SelectSingleNode("ReadJwtTimeoutSeconds").InnerText);

            //grpcNode
            var grpcNode = root.SelectSingleNode("GrpcOption");
            //Grpc Masters节点
            var grpcMasterNode = grpcNode.SelectSingleNode("GrpcMasters");
            //查询全部的Master节点
            var grpcServerAddressNodes = grpcMasterNode.SelectNodes("ServerAddress");
            foreach (XmlNode grpcServerAddressNode in grpcServerAddressNodes)
            {
                var ipAddress = grpcServerAddressNode.SelectSingleNode("IPAddress").InnerText;
                var port = int.Parse(grpcServerAddressNode.SelectSingleNode("Port").InnerText);
                option.GrpcOption.GrpcMasters.Add(new ServerAddress(ipAddress, port));
            }
            //Grpc同步MasterLeader时间间隔
            option.GrpcOption.SyncGrpcMasterLeaderInterval = int.Parse(grpcNode.SelectSingleNode("SyncGrpcMasterLeaderInterval").InnerText);
            //是否启用Grpc Tls
            option.GrpcOption.EnableTls = bool.Parse(grpcNode.SelectSingleNode("EnableTls").InnerText);
            //Grpc CA证书位置
            option.GrpcOption.Ca = grpcNode.SelectSingleNode("Ca").InnerText;
            //Master证书位置
            option.GrpcOption.MasterCert = grpcNode.SelectSingleNode("MasterCert").InnerText;
            //Master证书Key
            option.GrpcOption.MasterKey = grpcNode.SelectSingleNode("MasterKey").InnerText;
            //Volume证书位置
            option.GrpcOption.VolumeCert = grpcNode.SelectSingleNode("VolumeCert").InnerText;
            //Volume证书Key
            option.GrpcOption.VolumeKey = grpcNode.SelectSingleNode("VolumeKey").InnerText;
            //Filer证书位置
            option.GrpcOption.FilerCert = grpcNode.SelectSingleNode("FilerCert").InnerText;
            //Filer证书Key
            option.GrpcOption.FilerKey = grpcNode.SelectSingleNode("FilerKey").InnerText;

            //关闭读取流
            reader.Close();
            return option;
        }


        /// <summary>将配置文件转换成xml字符串
        /// </summary>
        public static string ToXml(SeaweedfsOption option)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<Seaweedfs>");
                sb.AppendLine(ParseNote("RestOption配置"));
                sb.AppendLine("<RestOption>");
                sb.AppendLine("<Masters>");
                foreach (var serverAddress in option.RestOption.Masters)
                {
                    sb.AppendLine("<ServerAddress>");
                    sb.Append("<IPAddress>");
                    sb.Append(serverAddress.IPAddress);
                    sb.AppendLine("</IPAddress>");

                    sb.Append("<Port>");
                    sb.Append(serverAddress.Port);
                    sb.AppendLine("</Port>");

                    sb.AppendLine("</ServerAddress>");
                }
                sb.AppendLine("</Masters>");

                //架构
                sb.Append("<Schema>");
                sb.AppendLine(option.RestOption.Scheme);
                sb.AppendLine("</Schema>");

                //Master Leader同步时间间隔
                sb.Append("<SyncMasterLeaderInterval>");
                sb.AppendLine(option.RestOption.SyncMasterLeaderInterval.ToString());
                sb.AppendLine("</SyncMasterLeaderInterval>");

                //Master Leader同步时间间隔
                sb.Append("<SyncMasterLeaderInterval>");
                sb.AppendLine(option.RestOption.SyncMasterLeaderInterval.ToString());
                sb.AppendLine("</SyncMasterLeaderInterval>");

                //是否开启Jwt认证
                sb.Append("<EnableJwt>");
                sb.AppendLine(option.RestOption.EnableJwt.ToString());
                sb.AppendLine("</EnableJwt>");

                //Jwt超时时间
                sb.Append("<JwtTimeoutSeconds>");
                sb.AppendLine(option.RestOption.JwtTimeoutSeconds.ToString());
                sb.AppendLine("</JwtTimeoutSeconds>");

                //是否开启读取Jwt认证
                sb.Append("<EnableReadJwt>");
                sb.AppendLine(option.RestOption.EnableReadJwt.ToString());
                sb.AppendLine("</EnableReadJwt>");

                //读取的Jwt超时时间
                sb.Append("<ReadJwtTimeoutSeconds>");
                sb.AppendLine(option.RestOption.ReadJwtTimeoutSeconds.ToString());
                sb.AppendLine("</ReadJwtTimeoutSeconds>");
                //RestOption End
                sb.AppendLine("</RestOption>");


                //GrpcOption
                sb.AppendLine("<GrpcOption>");
                sb.AppendLine("<GrpcMasters>");
                foreach (var serverAddress in option.GrpcOption.GrpcMasters)
                {
                    sb.AppendLine("<ServerAddress>");
                    sb.Append("<IPAddress>");
                    sb.Append(serverAddress.IPAddress);
                    sb.AppendLine("</IPAddress>");

                    sb.Append("<Port>");
                    sb.Append(serverAddress.Port);
                    sb.AppendLine("</Port>");

                    sb.AppendLine("</ServerAddress>");
                }
                sb.AppendLine("</GrpcMasters>");

                //Grpc 同步MasterLeader时间间隔
                sb.Append("<SyncGrpcMasterLeaderInterval>");
                sb.AppendLine(option.GrpcOption.SyncGrpcMasterLeaderInterval.ToString());
                sb.AppendLine("</SyncGrpcMasterLeaderInterval>");

                //是否启用Grpc Tls
                sb.Append("<EnableTls>");
                sb.AppendLine(option.GrpcOption.EnableTls.ToString());
                sb.AppendLine("</EnableTls>");
                //Ca证书位置
                sb.Append("<Ca>");
                sb.AppendLine(option.GrpcOption.Ca.ToString());
                sb.AppendLine("</Ca>");
                //Master证书位置
                sb.Append("<MasterCert>");
                sb.AppendLine(option.GrpcOption.MasterCert);
                sb.AppendLine("</MasterCert>");
                //Master证书Key
                sb.Append("<MasterKey>");
                sb.AppendLine(option.GrpcOption.MasterKey);
                sb.AppendLine("</MasterKey>");
                //Volume证书位置
                sb.Append("<VolumeCert>");
                sb.AppendLine(option.GrpcOption.VolumeCert);
                sb.AppendLine("</VolumeCert>");
                //Volume证书Key
                sb.Append("<VolumeKey>");
                sb.AppendLine(option.GrpcOption.VolumeKey);
                sb.AppendLine("</VolumeKey>");
                //Filer证书位置
                sb.Append("<FilerCert>");
                sb.AppendLine(option.GrpcOption.FilerCert);
                sb.AppendLine("</FilerCert>");
                //Filer证书Key
                sb.Append("<FilerKey>");
                sb.AppendLine(option.GrpcOption.FilerKey);
                sb.AppendLine("</FilerKey>");
                //GrpcOption End
                sb.AppendLine("</GrpcOption>");

                sb.AppendLine("</Seaweedfs>");
                return sb.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception($"生成Xml配置文件出错!{ex.Message}", ex);
            }

        }

        private static string ParseNote(string note)
        {
            return $"<![CDATA[{note}]]>";
        }
    }
}
