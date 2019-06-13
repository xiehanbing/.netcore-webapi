using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using General.Api.Core.Log;
using General.Core;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
using General.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// 异常处理 
    /// </summary>
    public class ExceptionFilter : IAsyncExceptionFilter  //IExceptionFilter, IFilterMetadata,
    {
        /// <summary>
        /// _logManager
        /// </summary>
        private readonly ILogManager _logManager;
        /// <summary>
        /// construct
        /// </summary>
        public ExceptionFilter(ILogManager logManager)
        {
            _logManager = logManager;
        }
     
        /// <summary>
        /// 获取是自定义异常类
        /// </summary>
        /// <param name="exception">异常类</param>
        /// <returns></returns>
        private MyException GetMyException(Exception exception)
        {
            if (exception is MyException myException)
                return myException;
            if (exception.InnerException == null) return null;
            return GetMyException(exception.InnerException);
        }
        /// <summary>
        /// 获取验证异常类
        /// </summary>
        /// <param name="exception">异常类</param>
        /// <returns></returns>
        private ValidatorException GetValidatorException(Exception exception)
        {
            if (exception is ValidatorException myException)
                return myException;
            if (exception.InnerException == null) return null;
            return GetValidatorException(exception.InnerException);
        }
        /// <summary>
        /// LogException 记录日志
        /// </summary>
        /// <param name="exContext"></param>
        private async Task LogException(ExceptionContext exContext)
        {

            var httpRequest = exContext.HttpContext;
            var request = await httpRequest.Request.ReadRequestAsync();
            var response = exContext.Exception.GetSerializeObject();
            var log = new ApiLog()
            {
                ConfirmNo = httpRequest.Request.Path.Value,
                ModelName = httpRequest.Request.Method,
                RequestContext = request,
                ResponseContext = response
            };
            _logManager.Info(log.GetSerializeObject());
            LogManage.ApiLog(log);
            return;
        }
        /// <summary>
        /// OnExceptionAsync
        /// </summary>
        /// <param name="context">content</param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {

            var requstUrl = context.HttpContext.Request.Path;
            //如果异常没有被处理
            if (context.ExceptionHandled == false)
            {
                var myException = GetMyException(context.Exception);

                if (myException != null)
                {
                    context.Result = new JsonResult(new ApiResult()
                    {
                        Success = false,
                        Message = myException.Message,
                        Code = myException.Code
                    });
                }
                else
                {
                    var validaException = GetValidatorException(context.Exception);
                    if (validaException != null)
                        context.Result = new JsonResult(new ValidatorResult()
                        {
                            Success = false,
                            Message = validaException.Message,
                            Code = validaException.Code,
                            Errors = validaException.Errors
                        });
                    else
                    {
                        //如果是系统异常
                        _logManager.Error(context.Exception);
                        context.Result = new JsonResult(new ApiResult()
                        {
                            Success = false,
                            Message = "系统异常",
                            Code = StatusCodes.Status500InternalServerError
                        });
                    }
                }
            }

            await LogException(context);
        
            context.ExceptionHandled = true;//异常已经处理了
        }
    }
}