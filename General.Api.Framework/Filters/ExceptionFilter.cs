using General.Core;
using General.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// 异常处理 
    /// </summary>
    public class ExceptionFilter : IExceptionFilter, IFilterMetadata
    {
        private ILogManager _logManager = EngineContext.CurrentEngin.Resolve<ILogManager>();
        /// <summary>
        /// 发生异常时的处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var requstUrl = context.HttpContext.Request.Path;
            //如果异常没有被处理
            if (context.ExceptionHandled == false)
            {
                //如果是自定义异常
                if (context.Exception is MyException myException)
                {
                    context.Result = new JsonResult(new ApiResult()
                    {
                        Success = false,
                        Message = myException.Message,
                        Code = myException.Code,

                    });
                }
                else if (context.Exception.InnerException != null && context.Exception is MyException myInnerException)
                {
                    context.Result = new JsonResult(new ApiResult()
                    {
                        Success = false,
                        Message = myInnerException.Message,
                        Code = myInnerException.Code
                    });
                }
                //如果是系统异常
                else
                {
                    _logManager.Error(context.Exception);
                    context.Result = new JsonResult(new ApiResult()
                    {
                        Success = false,
                        Message = "系统异常",
                        Code = StatusCodes.Status500InternalServerError
                    });
                }

            }
            context.ExceptionHandled = true;//异常已经处理了
        }
    }
}