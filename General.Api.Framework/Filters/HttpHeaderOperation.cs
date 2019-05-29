using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace General.Api.Framework.Filters
{
    public class HttpHeaderOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            var actionAttrs = context.ApiDescription.ActionAttributes();
            var isAuthorized = actionAttrs.Any(o => o.GetType() == typeof(AuthorizeAttribute));
            //提供action 都没有权限特性标记，检查控制器有没有
            if (isAuthorized == false)
            {
                var controllerAttrs = context.ApiDescription.ControllerAttributes();
                isAuthorized = controllerAttrs.Any(o => o.GetType() == typeof(AuthorizeAttribute));
            }

            var isAllowAnonymous = actionAttrs.Any(o => o.GetType() == typeof(AllowAnonymousAttribute));

            if (isAuthorized && isAllowAnonymous == false)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = false,
                    Description = "权限验证"
                });
            }
        }
    }
}