using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Api.Core.ApiAuthUser;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace General.Api.Application.Token
{
    /// <summary>
    /// token 接口
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IApiAuthUserDao _apiAuthUserDao;

        /// <summary>
        /// construct
        /// </summary>
        public TokenService(IApiAuthUserDao apiAuthUserDao)
        {
            _apiAuthUserDao = apiAuthUserDao;
        }
        /// <summary>
        /// <see cref="ITokenService.Get(string,string)"/>
        /// </summary>
        public async Task<bool> Get(string account, string password)
        {
            return await _apiAuthUserDao.VerifyUser(account, password);
        }
        /// <summary>
        /// 验证是否有权限
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="permission">权限</param>
        /// <returns></returns>
        public async Task<bool> ValidatePermission(string account, string permission)
        {
            return true;
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public async Task<List<string>> GetPermission(string account)
        {
            return new List<string>() { "All" };
        }
    }
}