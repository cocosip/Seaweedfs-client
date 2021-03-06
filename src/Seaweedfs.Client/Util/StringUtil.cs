using System.Text.RegularExpressions;

namespace Seaweedfs.Client.Util
{
    /// <summary>字符串工具类
    /// </summary>
    public static class StringUtil
    {
        /// <summary>从Fid中获取VolumeId
        /// </summary>
        public static string GetVolumeId(string fid)
        {
            var index = fid.IndexOf(",");
            if (index <= 0)
            {
                return fid;
            }
            return fid.Substring(0, index);
        }

        /// <summary>判断是否为Fid
        /// </summary>
        public static bool IsFid(string fid)
        {
            var patten = @"^\d{1,5}\,[A-Za-z0-9]{1,20}$";
            return Regex.IsMatch(fid, patten);
        }

    }
}
