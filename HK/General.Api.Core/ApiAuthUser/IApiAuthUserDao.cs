using System.Threading.Tasks;

namespace General.Api.Core.ApiAuthUser
{
    public interface IApiAuthUserDao
    {
        /// <summary>
        /// 验证apiauth 用户
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<bool> VerifyUser(string account, string password);
    }
}