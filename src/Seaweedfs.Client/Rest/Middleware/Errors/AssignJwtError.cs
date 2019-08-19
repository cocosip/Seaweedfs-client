namespace Seaweedfs.Client.Rest
{
    /// <summary>获取设置Assign Jwt错误
    /// </summary>
    public class AssignJwtError : Error
    {

        /// <summary>Ctor
        /// </summary>
        public AssignJwtError(string message) : base(message, (int)ErrorCodes.AssignJwtError)
        {

        }
    }
}
