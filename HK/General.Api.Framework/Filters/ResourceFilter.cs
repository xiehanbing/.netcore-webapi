using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ResourceFilter: IAsyncResourceFilter //IResourceFilter
    {
        private readonly IApiAuthUserDao _apiAuthUserDao;
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        public ResourceFilter(IApiAuthUserDao apiAuthUserDao, IHostingEnvironment environment, IConfiguration configuration)
        {
            _apiAuthUserDao = apiAuthUserDao;
            _environment = environment;
            _configuration = configuration;
        }
        //public void OnResourceExecuting(ResourceExecutingContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void OnResourceExecuted(ResourceExecutedContext context)
        //{
        //    throw new System.NotImplementedException();
        //}
        /// <summary>
        /// OnResourceExecutionAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var action = context.ActionDescriptor;
           //var sss=action.GetProperty<endpoint>()
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
                    if (!await _apiAuthUserDao.VerifyTokenAsync(token, account))
                    {
                        var result = new ApiResult() { Code = 401, Message = "授权验证未通过" };
                        context.Result = new JsonResult(result);
                        return;
                    }

                }
            }
            //这是可以实现进行过滤的
            //var requestPath = context.HttpContext.Request.Path.Value;
            //Console.WriteLine("ResourceFilter->request->url:" + requestPath);
            //var request = context.HttpContext.Request;
            //request.Body.WriteAsync()

            //if (requestPath.Contains("Values"))
            //{
            //    context.Result = new ObjectResult(new ApiResult(false, "不被允许"));
            //}
            //else
            //{
            //    await next();
            //}
            //var authToken = context.HttpContext.Request.Headers.TryGetValue("Authorization",out StringValues authTokenValue);
            //if (!authToken)
            //{
            //}

            await next();



            //await 
        }
    }
}