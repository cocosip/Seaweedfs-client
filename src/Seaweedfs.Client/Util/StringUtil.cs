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
                return "";
            }
            return fid.Substring(0, index);
        }

    }
}
