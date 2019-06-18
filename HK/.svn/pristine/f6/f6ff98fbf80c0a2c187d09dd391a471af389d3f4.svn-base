using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// HttpHeaderOperation
    /// </summary>
    public class HttpHeaderOperation : IOperationFilter
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// construct
        /// </summary>
        public HttpHeaderOperation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="operation">operation</param>
        /// <param name="context">context</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            bool isAuthorized = false;
            if (context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                if (methodInfo.CustomAttributes.Any(t => t.AttributeType == typeof(AllowAnonymousAttribute)))
                {
                    isAuthorized = false;
                }
                else
                {
                    if (methodInfo.ReflectedType != null)
                        isAuthorized =
                            methodInfo.ReflectedType.CustomAttributes.Any(t =>
                                t.AttributeType == typeof(AuthorizeAttribute));
                }
            }
            if (isAuthorized)
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

        #region 已过时
        ///// <summary>
        ///// Apply
        ///// </summary>
        ///// <param name="operation"></param>
        ///// <param name="context"></param>
        //public void Apply(Operation operation, OperationFilterContext context)
        //{
        //    if (operation.Parameters == null)
        //    {
        //        operation.Parameters = new List<IParameter>();
        //    }


        //    var actionAttrs = context.ApiDescription.ActionAttributes();
        //    var isAuthorized = actionAttrs.Any(o => o.GetType() == typeof(AuthorizeAttribute));
        //    //提供action 都没有权限特性标记，检查控制器有没有
        //    if (isAuthorized == false)
        //    {
        //        var controllerAttrs = context.ApiDescription.ControllerAttributes();
        //        isAuthorized = controllerAttrs.Any(o => o.GetType() == typeof(AuthorizeAttribute));
        //    }

        //    var isAllowAnonymous = actionAttrs.Any(o => o.GetType() == typeof(AllowAnonymousAttribute));

        //    if (isAuthorized && isAllowAnonymous == false)
        //    {
        //        operation.Parameters.Add(new NonBodyParameter()
        //        {
        //            Name = "Authorization",
        //            In = "header",
        //            Type = "string",
        //            Required = false,
        //            Description = "权限验证"
        //        });
        //    }
        //}


        #endregion
    }
}