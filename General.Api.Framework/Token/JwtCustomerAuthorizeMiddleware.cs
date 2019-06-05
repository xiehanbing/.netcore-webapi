using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace General.Api.Framework.Token
{
    /// <summary>
    /// 自定义授权中间件 (暂不使用)
    /// </summary>
    public class JwtCustomerAuthorizeMiddleware
    {
        private readonly RequestDelegate next;

        public JwtCustomerAuthorizeMiddleware(RequestDelegate next, string secret, List<string> anonymousPathList)
        {
            #region   设置自定义jwt 的秘钥
            if (!string.IsNullOrEmpty(secret))
            {
                TokenContext.SecurityKey = secret;
            }
            #endregion
            this.next = next;
            UserContext.AllowAnonymousPathList.AddRange(anonymousPathList);
        }

        public async Task Invoke(HttpContext context, UserContext userContext, IOptions<JwtOption> optionContainer)
        {
            if (userContext.IsAllowAnonymous(context.Request.Path))
            {
                await next(context);
                return;
            }

            var option = optionContainer.Value;

            #region 身份验证，并设置用户Ruser值

            var result = context.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            if (!result || string.IsNullOrEmpty(authStr.ToString()))
            {
                throw new UnauthorizedAccessException("未授权");
            }

            string user = "";
            result = TokenContext.Validate(authStr.ToString().Substring("Bearer ".Length).Trim(),  payLoad =>
            {
                var success = true;
                user = payLoad["aud"]?.ToString();
                //可以添加一些自定义验证，用法参照测试用例
                //验证是否包含aud 并等于 roberAudience
                success =payLoad["aud"]?.ToString() == option.Audience;
                if (success)
                {
                    //设置Ruse值,把user信息放在payLoad中，（在获取jwt的时候把当前用户存放在payLoad的ruser键中）
                    //如果用户信息比较多，建议放在缓存中，payLoad中存放缓存的Key值
                    userContext.TryInit(payLoad["ruser"]?.ToString());
                }
                return success;
            });
            if (!result)
            {
                throw new UnauthorizedAccessException("未授权");
            }

            #endregion
            #region 权限验证
            var permissionVali = userContext.Authorize(user, context.Request.Path);
            if (!permissionVali)
            {
                throw new UnauthorizedAccessException("未授权");
            }
            #endregion

            await next(context);
        }
    }
}