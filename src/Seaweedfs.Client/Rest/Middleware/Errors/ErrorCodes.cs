namespace Seaweedfs.Client.Rest
{
    /// <summary>错误编码
    /// </summary>
    public enum ErrorCodes
    {
        /// <summary>创建Http请求信息错误
        /// </summary>
        HttpBuildError = 2,

        /// <summary> Execute执行错误
        /// </summary>
        ExecuteError = 3,

        /// <summary>获取设置Assign Jwt错误
        /// </summary>
        AssignJwtError = 4,

        /// <summary>获取设置Lookup Jwt错误
        /// </summary>
        LookupJwtError = 5

    }
}
