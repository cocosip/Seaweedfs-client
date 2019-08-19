using System;

namespace Seaweedfs.Client.Rest
{
    /// <summary>JwtToken
    /// </summary>
    public class JwtToken
    {
        /// <summary>文件Fid
        /// </summary>
        public string Fid { get; set; }

        /// <summary>Jwt的值
        /// </summary>
        public string Jwt { get; set; }

        /// <summary>创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>Ctor
        /// </summary>
        public JwtToken()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public JwtToken(string fid, string jwt)
        {
            Fid = fid;
            Jwt = jwt;
            CreationTime = DateTime.Now;
        }

        /// <summary>是否超时
        /// </summary>
        public bool IsTimeout(int timeoutSeconds)
        {
            return (int)(DateTime.Now - CreationTime).TotalSeconds > timeoutSeconds;
        }
    }
}
