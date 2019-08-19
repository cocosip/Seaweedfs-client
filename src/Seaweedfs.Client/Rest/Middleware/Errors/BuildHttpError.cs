namespace Seaweedfs.Client.Rest
{
    /// <summary>创建Http错误
    /// </summary>
    public class BuildHttpError : Error
    {
        /// <summary>Ctor
        /// </summary>
        public BuildHttpError(string message) : base(message, (int)ErrorCodes.HttpBuildError)
        {
        }
    }
}
