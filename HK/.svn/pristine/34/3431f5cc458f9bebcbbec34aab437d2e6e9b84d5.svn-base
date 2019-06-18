using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using General.Api.Core.Log;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace General.Api.Framework.Middleware
{
    /// <summary>
    /// HttpHandleMiddleware
    /// </summary>
    public class HttpHandleMiddleware
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// ErrorHandlingMiddleware
        /// </summary>
        public HttpHandleMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {

            context.Response.OnCompleted(async o =>
            {
                var c = o as HttpContext;
                if (c != null)
                {
                    var request = await c.Request.ReadRequestAsync();
                    var retStr = await c.Response.ReadBodyAsync();
                    LogManage.ApiLog(new ApiLog()
                    {
                        ConfirmNo = c.Request.Path.Value,
                        ModelName = c.Request.Method,
                        RequestContext = request,
                        ResponseContext = retStr
                    });
                }
            }, context);
            await next(context);


        }


    }
}