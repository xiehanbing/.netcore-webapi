using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Api.Application.Token;
using General.Api.Framework;
using General.Core;
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
        [HttpPost,Route("token")]
        public async Task<ActionResult> RequestToken([FromBody] TokenRequest request)
        {
            var data = await _tokenService.Get(request.Account, request.Password);
            //验证不通过
            if (!data)
            {
                return Unauthorized("Could not verify username and password");
            }

            return Ok(new
            {
                token = GeneralToken.TokenHandle.CreateToken(_configuration, request)
            });
        }
    }
}