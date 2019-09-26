using System;
using System.ComponentModel;
using System.Threading.Tasks;
using General.Api.Core.Log;
using General.Core;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
using General.Log;
using Microsoft.AspNetCore.Http;

namespace General.Api.Framework.Middleware
{
    /// <summary>
    /// ErrorHandlingMiddleware 错误处理中心
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogManager _logManager;
        /// <summary>
        /// ErrorHandlingMiddleware
        /// </summary>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogManager logManager)
        {
            this.next = next;
            _logManager = logManager;
        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {

                var statusCode = context.Response.StatusCode;
                if (e is UnauthorizedAccessException)
                {
                    statusCode = 401;
                }
                _logManager.Error(e.GetSerializeObject());
                await HandleExceptionAsync(context, statusCode, e.Message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                string msg = typeof(ExceptionCode).GetEnumDescription(statusCode);
                if (msg.IsNotWhiteSpace())
                {
                    msg = "未知错误";
                }

                await HandleExceptionAsync(context, statusCode, msg);
            }
        }
        /// <summary>
        /// HandleExceptionAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new ApiResult()
            {
                Code = statusCode,
                Success = false,
                Message = msg
            };
            var result = data.GetSerializeObject();
            //LogException(context, result).Wait();
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// LogException 记录日志
        /// </summary>
        private static async Task LogException(HttpContext context, string response)
        {
            var path = context.Request.Path.Value;
            if (path.ToLower().Contains("swagger"))
            {
                return;
            }
            var request = await context.Request.ReadRequestAsync();
            LogManage.ApiLog(new ApiLog()
            {
                ConfirmNo = context.Request.Path.Value,
                ModelName = context.Request.Method,
                RequestContext = request,
                ResponseContext = response
            });
            return;
        }
    }
    /// <summary>
    /// exceptionCode
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        UnAuth = 401,
        /// <summary>
        /// 未找到服务
        /// </summary>
        [Description("未找到服务")]
        NotFoundService = 404,
        /// <summary>
        /// 请求错误
        /// </summary>
        [Description("请求错误")]
        RequestError = 502
    }
}