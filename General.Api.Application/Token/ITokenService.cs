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
    }
}