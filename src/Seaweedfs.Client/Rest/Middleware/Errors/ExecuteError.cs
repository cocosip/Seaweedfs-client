namespace Seaweedfs.Client.Rest
{
    /// <summary>执行错误
    /// </summary>
    public class ExecuteError : Error
    {

        /// <summary>Ctor
        /// </summary>
        public ExecuteError(string message) : base(message, (int)ErrorCodes.ExecuteError)
        {

        }
    }
}
