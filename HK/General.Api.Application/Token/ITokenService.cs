using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace General.Api.Application.Token
{
    /// <summary>
    /// token 接口
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<bool> Get(string account, string password);
        /// <summary>
        /// 验证是否有权限
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        Task<bool> ValidatePermission(string account, string permission);
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        Task<List<string>> GetPermission(string account);
        /// <summary>
        /// 添加新的token 记录
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        Task<bool> AddTokenRecord(string account, string token);
    }
}