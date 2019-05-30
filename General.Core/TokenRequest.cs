namespace General.Core
{
    /// <summary>
    /// token请求类
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string  Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string  Password { get; set; }
    }
}