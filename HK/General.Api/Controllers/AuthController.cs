using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Api.Application.Token;
using General.Api.Framework;
using General.Api.Framework.Filters;
using General.Core;
using General.Core.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using GeneralToken = General.Api.Framework.Token;
namespace General.Api.Controllers
{
    /// <summary>
    /// 权限获取
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        /// <summary>
        /// 权限构造函数
        /// </summary>
        public AuthController(IConfiguration configuration, ITokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("token")]
        public async Task<ActionResult> RequestToken([FromBody] TokenRequest request)
        {
            var data = await _tokenService.Get(request.Account, request.Password);
            //验证不通过
            if (!data)
            {
                return Unauthorized("Could not verify username and password");
            }

            var token = GeneralToken.TokenHandle.CreateToken(_configuration, request);
            if (await _tokenService.AddTokenRecordAsync(request.Account, token))
            {
                return Ok(new
                {
                    token = token
                });
            }
            throw new MyException("记录token 失败");
        }
        /// <summary>
        /// 清楚token魂村
        /// </summary>
        /// <param name="key">key</param>
        [HttpGet, Route("cache/remove"),GeneralAdminAuthorize]
        public bool ClearMemoryCatch(string key)
        {
            if (!key.IsNotWhiteSpace()) throw new MyException("key 不允许为空");
           return _tokenService.ClearMemaryCache(key);
        }
        /// <summary>
        /// 添加apiauth 账号
        /// </summary>
        /// <param name="user">参数</param>
        [HttpPost, Route("user"), GeneralAdminAuthorize]
        public async Task<bool> AddApiAuthUser(ApiAuthUserDto user)
        {
            return await _tokenService.AddApiAuthUserAsync(user.Account, user.Password);
        }

        /// <summary>
        /// 添加admin账号
        /// </summary>
        /// <param name="user">参数</param>
        [HttpPost, Route("user/admin"), GeneralAdminAuthorize]
        public async Task<bool> AddAdminApiAuthUser(ApiAuthUserDto user)
        {
            return await _tokenService.AddApiAuthUserAsync(user.Account, user.Password, true);
        }
    }
}