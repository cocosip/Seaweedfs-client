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

            //Masters节点
            var mastersNode = root.SelectSingleNode("Masters");
            //查询全部的Master节点
            var masterNodes = mastersNode.SelectNodes("Master");
            foreach (XmlNode masterNode in masterNodes)
            {
                var ipAddress = masterNode.SelectSingleNode("IPAddress").InnerText;
                var port = int.Parse(masterNode.SelectSingleNode("Port").InnerText);
                option.Masters.Add(new MasterServer(ipAddress, port));
            }
            //架构,http/https
            option.Scheme = root.SelectSingleNode("Scheme").InnerText;
            //Master同步时间间隔
            option.SyncMasterLeaderInterval = int.Parse(root.SelectSingleNode("SyncMasterLeaderInterval").InnerText);
            //是否启用jwt
            option.EnableJwt = bool.Parse(root.SelectSingleNode("EnableJwt").InnerText);
            //Jwt超时时间
            option.JwtTimeoutSeconds = int.Parse(root.SelectSingleNode("JwtTimeoutSeconds").InnerText);

            //是否启用读取文件jwt
            option.EnableReadJwt = bool.Parse(root.SelectSingleNode("EnableReadJwt").InnerText);
            //读文件Jwt
            option.ReadJwtTimeoutSeconds = int.Parse(root.SelectSingleNode("ReadJwtTimeoutSeconds").InnerText);

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
                sb.AppendLine(ParseNote("Master信息,可以配置多台"));
                sb.AppendLine("<Masters>");
                foreach (var master in option.Masters)
                {
                    sb.AppendLine("<Master>");
                    sb.Append("<IPAddress>");
                    sb.Append(master.IPAddress);
                    sb.AppendLine("</IPAddress>");

                    sb.Append("<Port>");
                    sb.Append(master.Port);
                    sb.AppendLine("</Port>");

                    sb.AppendLine("</Master>");

                }
                sb.AppendLine("</Masters>");

                //架构
                sb.Append("<Schema>");
                sb.AppendLine(option.Scheme);
                sb.AppendLine("</Schema>");

                //Master Leader同步时间间隔
                sb.Append("<SyncMasterLeaderInterval>");
                sb.AppendLine(option.SyncMasterLeaderInterval.ToString());
                sb.AppendLine("</SyncMasterLeaderInterval>");

                //Master Leader同步时间间隔
                sb.Append("<SyncMasterLeaderInterval>");
                sb.AppendLine(option.SyncMasterLeaderInterval.ToString());
                sb.AppendLine("</SyncMasterLeaderInterval>");

                //是否开启Jwt认证
                sb.Append("<EnableJwt>");
                sb.AppendLine(option.EnableJwt.ToString());
                sb.AppendLine("</EnableJwt>");

                //Jwt超时时间
                sb.Append("<JwtTimeoutSeconds>");
                sb.AppendLine(option.JwtTimeoutSeconds.ToString());
                sb.AppendLine("</JwtTimeoutSeconds>");

                //是否开启读取Jwt认证
                sb.Append("<EnableReadJwt>");
                sb.AppendLine(option.EnableReadJwt.ToString());
                sb.AppendLine("</EnableReadJwt>");

                //读取的Jwt超时时间
                sb.Append("<ReadJwtTimeoutSeconds>");
                sb.AppendLine(option.ReadJwtTimeoutSeconds.ToString());
                sb.AppendLine("</ReadJwtTimeoutSeconds>");

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
