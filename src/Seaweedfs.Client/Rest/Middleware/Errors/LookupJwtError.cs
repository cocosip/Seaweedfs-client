namespace Seaweedfs.Client.Rest
{
    /// <summary>Lookup查询错误
    /// </summary>
    public class LookupJwtError : Error
    {
        /// <summary>Ctor
        /// </summary>
        public LookupJwtError(string message) : base(message, (int)ErrorCodes.LookupJwtError)
        {

        }
    }
}
