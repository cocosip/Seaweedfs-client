namespace Seaweedfs.Client.Extensions
{
    /// <summary>String类型扩展
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>判断String类型是否为空
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>判断String类型是否为空或者空格
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
