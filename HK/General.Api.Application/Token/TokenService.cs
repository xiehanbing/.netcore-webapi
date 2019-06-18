using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Api.Core.ApiAuthUser;
using General.Api.Core.Log;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        /// <summary>
        /// construct
        /// </summary>
        public TokenService(IApiAuthUserDao apiAuthUserDao, IMemoryCache memoryCache)
        {
            _apiAuthUserDao = apiAuthUserDao;
            _memoryCache = memoryCache;
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
        /// <summary>
        /// <see cref="ITokenService.AddTokenRecordAsync(string,string)"/>
        /// </summary>
        public async Task<bool> AddTokenRecordAsync(string account, string token)
        {
            var data = await _apiAuthUserDao.AddTokenAsync(account, token);
            if (data)
            {
                _memoryCache.Set(account + "token", token, TimeSpan.FromHours(3));
            }
            return data;
        }
        /// <summary>
        /// <see cref="ITokenService.VerifyTokenAsync(string,string)"/>
        /// </summary>
        public async Task<bool> VerifyTokenAsync(string account, string token)
        {
            var tokenValue = await GetTokenMemory(account);
            if (tokenValue.IsNotWhiteSpace())
            {
                return token.Trim().Equals(tokenValue.Trim());
            }
            return false;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="account">account</param>
        /// <returns></returns>
        private async Task<string> GetTokenMemory(string account)
        {
            var memory = _memoryCache.TryGetValue(account + "token", out string tokenValue);
            if (memory && tokenValue.IsNotWhiteSpace())
                return tokenValue;
            var data = await _apiAuthUserDao.GetTokenAsync(account);
            if (data != null)
            {
                SetTokenMemory(account, data.JwtToken);
            }
            else
                LogManage.ApiLog(new ApiLog()
                {
                    ConfirmNo = account,
                    ModelName = "GetTokenMemory",
                    RequestContext = account,
                    ResponseContext = $"{account} 不存在token"
                });

            return data?.JwtToken;
        }

        /// <summary>
        /// 设置token 魂村
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        private void SetTokenMemory(string account, string token)
        {
            _memoryCache.Set(account + "token", token, TimeSpan.FromHours(3));
        }

    }
}