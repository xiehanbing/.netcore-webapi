using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Api.Core.ApiAuthUser;
using General.Api.Core.Log;
using General.Core;
using General.Core.Encrypt;
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
            return await _apiAuthUserDao.VerifyUser(account, password.Sha256Base64Encry());
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
            Console.WriteLine("password encry:"+("123456").Sha256Base64Encry());
            var data = await _apiAuthUserDao.AddTokenAsync(account, token);
            if (data)
            {
               SetTokenMemory(account,token);
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
            {
                Console.WriteLine("cache is exist");
                return tokenValue;
            }

            var data = await _apiAuthUserDao.GetTokenAsync(account);
            if (data != null)
            {
                Console.WriteLine("cache is not exist");
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
            Console.WriteLine(account+"cache is set");
        }
        /// <summary>
        /// <see cref="ITokenService.ClearMemaryCache(string)"/>
        /// </summary>
        public bool ClearMemaryCache(string key)
        {
            _memoryCache.Remove(key);
            Console.WriteLine($"{key} cache is clear");
            return true;
        }
        /// <summary>
        /// <see cref="ITokenService.VerifyAdminAsync(string,string)"/>
        /// </summary>
        public async Task<bool> VerifyAdminAsync(string account, string password)
        {
            var passwordEncry = password.Sha256Base64Encry();
            return await _apiAuthUserDao.VerifyAdmin(account, passwordEncry);
        }
        /// <summary>
        /// <see cref="ITokenService.AddApiAuthUserAsync(string,string,bool)"/>
        /// </summary>
        public async Task<bool> AddApiAuthUserAsync(string account, string password,bool isAdmin=false)
        {
            var isExist = await _apiAuthUserDao.GetAuthUserAsync(account);
            if(isExist!=null)throw new MyException("用户已存在");
            return await _apiAuthUserDao.AddUserAsync(account, password.Sha256Base64Encry(), isAdmin);
        }
    }
}