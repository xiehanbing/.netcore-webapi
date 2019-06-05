using System;
using System.Linq;
using System.Threading.Tasks;
using General.Core.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace General.Api.Framework.Token
{
    /// <summary>
    /// 访问者年龄要求自定义策略
    /// </summary>
    public class AgeRequireHandler : AuthorizationHandler<AgeRequireMent>
    {
        private readonly IConfiguration _configuration;

        public AgeRequireHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequireMent requirement)
        {

            if (!context.User.HasClaim(c => c.Type == "age"))
            {
                context.Fail();
                return Task.FromResult(0);
                // return Task.CompletedTask;
            }
            int.TryParse(context.User.Claims?.FirstOrDefault(c => c.Type == "age")?.Value, out int age);

            if (age >= requirement.Age)
            {
                context.Succeed(requirement);//标识验证成功

            }
            else
            {
                context.Fail();
                return Task.FromResult(0);
            }
            return Task.CompletedTask;
        }
    }
    public class AgeRequireMent : IAuthorizationRequirement
    {
        public int Age { get; set; }
        public AgeRequireMent(int age) => this.Age = age;
    }
}