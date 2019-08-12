using System;
using System.Collections.Generic;
using System.Text;

namespace Seaweedfs.Client.Rest
{
    /// <summary>分配的Key
    /// </summary>
    public class AssignFileKey
    {
        /// <summary>文件Fid
        /// </summary>
        public string Fid { get; set; }

        /// <summary>Url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>Ctor
        /// </summary>
        public AssignFileKey()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public AssignFileKey(string fid, string url)
        {
            Fid = fid;
            Url = url;
        }

    }
}
