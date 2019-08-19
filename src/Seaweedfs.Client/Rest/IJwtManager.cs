namespace Seaweedfs.Client.Rest
{
    /// <summary>Jwt管理
    /// </summary>
    public interface IJwtManager
    {
        /// <summary>添加Assign Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="jwt">Jwt的值</param>
        void AddAssignJwt(string fid, string jwt);

        /// <summary>根据Fid获取Assign Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <returns></returns>
        string GetAssignJwt(string fid);

        /// <summary>添加Lookup Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="jwt">Jwt的值</param>
        void AddLookupJwt(string fid, string jwt);

        /// <summary>根据Fid获取Lookup Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <returns></returns>
        string GetLookupJwt(string fid);

        /// <summary>添加读文件Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <param name="jwt">Jwt的值</param>
        void AddReadJwt(string fid, string jwt);

        /// <summary>根据Fid获取Lookup Jwt
        /// </summary>
        /// <param name="fid">文件Fid</param>
        /// <returns></returns>
        string GetReadJwt(string fid);

        /// <summary>运行
        /// </summary>
        void Start();

        /// <summary>停止
        /// </summary>
        void Shutdown();
    }
}
