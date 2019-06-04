using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Api.Framework;
using General.Api.Framework.Token;
using General.Core.Extension;
using General.Core.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace General.Api
{
    /// <summary>
    /// jwt token customer handle  jwt 自定义
    /// </summary>
    public static class JwtConfigServiceExtension
    {
        /// <summary>
        /// 添加自定义的 jwt token 验证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddInnerAuthorize(this IServiceCollection services, IConfiguration config)
        {

            services.AddAuthorization(option =>
            {
                //自定义一些策略，原理都是基于申明key和value的值进行比较或者是否有无
                #region 键值对对比的一些验证策略
                option.AddPolicy("onlyRober", policy => policy.RequireClaim("name", "general"));
                //角色校验
                option.AddPolicy("onlyAdminOrSuperUser", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "SuperUser"));
                //多申明共同,申明中包含aud：general 或者申明中值有等于general的都可以通过
                option.AddPolicy("multiClaim", policy => policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim("aud", config["Jwt:Audience"]) || context.User.HasClaim(c => c.Value == config["Jwt:Name"]);
                }));
                #endregion

                #region 自定义验证策略
                option.AddPolicy("ageRequire", policy => policy.Requirements.Add(new AgeRequireMent(20)));
                option.AddPolicy("General", policy => policy.Requirements.Add(new CommonAuthorize()));
                #endregion


            }).AddAuthentication(option =>
            {
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                if (!string.IsNullOrEmpty(config["Jwt:SecurityKey"]))
                {
                    TokenContext.SecurityKey = config["Jwt:SecurityKey"];
                }
                //设置需要验证的项目
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = config["Jwt:Audience"],//Audience
                    ValidIssuer = config["Jwt:Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenContext.SecurityKey))//拿到SecurityKey
                };
                option.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        var payload = new ApiResult
                        {
                            Success = false,
                            Code = 401,
                            Message =
                                "很抱歉，您无权访问该接口"
                        }.GetSerializeObject();
                        //自定义返回的数据类型
                        context.Response.ContentType = "application/json";
                        //自定义返回状态码，默认为401 我这里改成 200
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //输出Json数据结果
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };
            });

            //自定义策略IOC添加
            services.AddSingleton<IAuthorizationHandler, AgeRequireHandler>();
            services.AddSingleton<IAuthorizationHandler, CommonAuthorizeHandler>();
        }
    }

}