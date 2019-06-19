using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Application.Token;
using General.Api.Core.ApiAuthUser;
using General.Core.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// ResourceFilter
    /// </summary>
    public class ResourceFilter : IAsyncResourceFilter //IResourceFilter
    {
        private readonly ITokenService _tokenService;
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        public ResourceFilter(ITokenService tokenService, IHostingEnvironment environment, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _environment = environment;
            _configuration = configuration;
        }
        /// <summary>
        /// OnResourceExecutionAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var action = context.ActionDescriptor;
            var haveAdminAuth = action.FilterDescriptors.Any(o => o.Filter.ToString() == typeof(GeneralAdminAuthorizeAttribute).ToString());
            if (haveAdminAuth)
            {
                var authSuccess =
                    context.HttpContext.Request.Headers.TryGetValue("Authorization",
                        out StringValues authValue);
                if (!authSuccess)
                {
                    var result = new ApiResult() { Code = 401, Message = "Authorization不能为空" };
                    context.Result = new JsonResult(result);
                    return;
                }
                var token = authValue.ToString().Trim();
                var jwtArr = token.Split("||");
                if (jwtArr.Length < 2)
                {
                    var result = new ApiResult() { Code = 401, Message = "Authorization不符合规范" };
                    context.Result = new JsonResult(result);
                    return;
                }
                var account = jwtArr[0];
                var password = jwtArr[1];
                if (!await _tokenService.VerifyAdminAsync(account, password))
                {
                    var result = new ApiResult() { Code = 401, Message = "Admin授权验证未通过" };
                    context.Result = new JsonResult(result);
                    return;
                }
            }
            else
            {
                var haveAuthor = action.FilterDescriptors.Any(o => o.Filter.ToString() == typeof(GeneralAuthorizeAttribute).ToString());
                var haveAllowAny = action.FilterDescriptors.Any(o => o.Filter.ToString() == typeof(AllowAnonymousFilter).ToString());
                bool needAuth = false;
                if (_configuration["needAuth"] != null && (bool.TryParse(_configuration["needAuth"], out needAuth)) && !haveAllowAny && haveAuthor)
                {
                    if (needAuth)
                    {
                        var authSuccess =
                            context.HttpContext.Request.Headers.TryGetValue("Authorization",
                                out StringValues authValue);
                        if (!authSuccess)
                        {
                            var result = new ApiResult() { Code = 401, Message = "Authorization不能为空" };
                            context.Result = new JsonResult(result);
                            return;
                        }

                        if (!authValue.ToString()?.Contains("Bearer") ?? false)
                        {
                            var result = new ApiResult() { Code = 401, Message = "Authorization不符合规范" };
                            context.Result = new JsonResult(result);
                            return;
                        }

                        var token = authValue.ToString().Substring("Bearer ".Length).Trim();
                        var jwtArr = token.Split('.');
                        //var header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
                        Dictionary<string, object> payLoad;
                        try
                        {
                            payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));
                        }
                        catch (Exception)
                        {
                            var result = new ApiResult() { Code = 401, Message = "Authorization不符合规范" };
                            context.Result = new JsonResult(result);
                            return;
                        }
                        var account = payLoad["sid"]?.ToString();
                        if (account == null)
                        {
                            var result = new ApiResult() { Code = 401, Message = "未找到sid" };
                            context.Result = new JsonResult(result);
                            return;
                        }
                        if (!await _tokenService.VerifyTokenAsync(account, token))
                        {
                            var result = new ApiResult() { Code = 401, Message = "授权验证未通过" };
                            context.Result = new JsonResult(result);
                            return;
                        }

                    }
                }
            }

            await next();
        }
    }
}