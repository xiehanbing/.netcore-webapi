using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using General.Api.Core.ApiAuthUser;
using General.Api.Framework.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// GeneralAuthorizationFilter
    /// </summary>
    public class GeneralAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IApiAuthUserDao _apiAuthUserDao;
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiAuthUserDao"></param>
        public GeneralAuthorizationFilter(IApiAuthUserDao apiAuthUserDao, IHostingEnvironment environment, IConfiguration configuration)
        {
            _apiAuthUserDao = apiAuthUserDao;
            _environment = environment;
            _configuration = configuration;
        }
        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var action = context.ActionDescriptor;
            var haveAuthor = action.FilterDescriptors.Any(o => o.Filter.ToString() == typeof(AuthorizeFilter).ToString());
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
                        var result = new ApiResult() { Code = 401, Message = "未授权验证" };
                        context.Result = new JsonResult(result);
                        return;
                    }

                    var token = authValue.ToString().Substring("Bearer ".Length).Trim();
                    var jwtArr = token.Split('.');
                    //var header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
                    Dictionary<string,object> payLoad;
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
                    var account =payLoad["sid"]?.ToString();
                    if (account == null)
                    {
                        var result = new ApiResult() { Code = 401, Message = "未找到sid" };
                        context.Result = new JsonResult(result);
                        return;
                    }
                    if (!_apiAuthUserDao.VerifyToken(token, account))
                    {
                        var result = new ApiResult() { Code = 401, Message = "授权验证未通过" };
                        context.Result = new JsonResult(result);
                        return;
                    }

                }
            }
        }




    }
}
