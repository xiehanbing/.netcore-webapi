using System.Linq;
using System.Threading.Tasks;
using General.Core.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace General.Api.Framework.Token
{
    /// <summary>
    /// 访问者年龄要求自定义策略
    /// </summary>
    public class AgeRequireHandler : AuthorizationHandler<AgeRequireMent>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequireMent requirement)
        {

            if (!context.User.HasClaim(c => c.Type == "age"))
            {
                return Task.CompletedTask;
            }
            int.TryParse(context.User.Claims?.FirstOrDefault(c => c.Type == "age")?.Value, out int age);

            if (age >= requirement.Age)
            {
                context.Succeed(requirement);//标识验证成功

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