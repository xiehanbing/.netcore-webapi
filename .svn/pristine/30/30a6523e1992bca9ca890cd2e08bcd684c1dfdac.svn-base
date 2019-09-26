using System.Threading.Tasks;
using General.EntityFrameworkCore.ApiAuthUser;

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
        /// <summary>
        /// 验证 token
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="account">account  账号</param>
        /// <returns></returns>
        bool VerifyToken(string token, string account);
        /// <summary>
        /// 验证 token  异步
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<bool> VerifyTokenAsync(string token, string account);
        /// <summary>
        /// 添加新的token
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        Task<bool> AddTokenAsync(string account, string token);
        /// <summary>
        /// 更新token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        Task<bool> UpdateTokenAsync(ApiAuthUserToken token);

        /// <summary>
        /// 移除token
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        Task<bool> RemoveTokenAsync(string account, string token);
        /// <summary>
        /// 获取用户的token
        /// </summary>
        /// <param name="account">account</param>
        /// <returns></returns>
        Task<ApiAuthUserToken> GetTokenAsync(string account);
        /// <summary>
        /// 验证是否为管理员
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="passwordEncry">password 加密版</param>
        /// <returns></returns>
        Task<bool> VerifyAdmin(string account, string passwordEncry);

        /// <summary>
        /// 添加apiauther 用户
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="password">password</param>
        /// <param name="isAdmin">isAdmin</param>
        /// <returns></returns>
        Task<bool> AddUserAsync(string account, string password, bool isAdmin = false);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        Task<EntityFrameworkCore.ApiAuthUser.ApiAuthUser> GetAuthUserAsync(string account);
    }
}