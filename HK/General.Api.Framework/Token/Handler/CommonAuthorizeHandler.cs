using System;
using System.Linq;
using System.Threading.Tasks;
using General.Api.Framework.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace General.Core.Token
{
    /// <summary>
    /// 自定义通用 jwt 验证 自定义策略形式实现自定义jwt验证
    /// </summary>
    public class CommonAuthorizeHandler : AuthorizationHandler<CommonAuthorize>
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;
        /// <summary>
        /// CommonAuthorizeHandler
        /// </summary>
        public CommonAuthorizeHandler(IConfiguration configuration,IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        /// <summary>
        /// 常用自定义验证策略，模仿自定义中间件JwtCustomerauthorizeMiddleware的验证范围
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommonAuthorize requirement)
        {
            //如果是开发环境 忽略
            //if (_environment.IsDevelopment())
            //{
            //    context.Succeed(requirement);
            //    return Task.CompletedTask;
            //}
            //如果不存在此配置或此配置 为false 忽略
            bool needAuth = false;
            if(_configuration["needAuth"]==null||(bool.TryParse(_configuration["needAuth"],out  needAuth) ))
            {
                if (!needAuth)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            var httpContext = (context.Resource as AuthorizationFilterContext)?.HttpContext;
            if (httpContext == null) throw new Exception("AuthHttpContext is null");
            var userContext = httpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;

            var jwtOption = (httpContext.RequestServices.GetService(typeof(IOptions<JwtOption>)) as IOptions<JwtOption>).Value;

            if (userContext == null) throw new Exception("UserContext is null");
            #region 身份验证，并设置用户Ruser值
            var result = httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            if (!result || string.IsNullOrEmpty(authStr.ToString()))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            if (!authStr.ToString()?.Contains("Bearer") ?? false)
            {
                context.Fail();
                return Task.FromResult(0);
            }
            var user = "";
            result = TokenContext.Validate(authStr.ToString().Substring("Bearer ".Length).Trim(), payLoad =>
            {
                var success = true;
                //可以添加一些自定义验证，用法参照测试用例
                //验证是否包含aud 并等于 roberAudience
                success = payLoad["aud"]?.ToString() == jwtOption.Audience;
                if (success)
                {
                    user = payLoad["ruser"]?.ToString();
                    //设置Ruse值,把user信息放在payLoad中，（在获取jwt的时候把当前用户存放在payLoad的ruser键中）
                    //如果用户信息比较多，建议放在缓存中，payLoad中存放缓存的Key值
                    userContext.TryInit(payLoad["ruser"]?.ToString());
                }
                return success;
            });
            if (!result)
            {
                context.Fail();
                
                return Task.CompletedTask;
            }

            #endregion
            #region 权限验证

            var permissionVali = userContext.Authorize(user, httpContext.Request.Path);
            if (!permissionVali)
            {
                context.Fail();
                return Task.FromResult(0);
            }
            #endregion
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
    /// <summary>
    /// CommonAuthorize
    /// </summary>
    public class CommonAuthorize : IAuthorizationRequirement
    {

    }
}