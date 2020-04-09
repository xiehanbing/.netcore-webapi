using General.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// 模型验证
    /// </summary>
    public class ModelValidFilter : IActionFilter
    {
        /// <summary>
        /// 处理开始
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                //var ajaxResult = new ApiResult();
                var message = "";
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        message += error.ErrorMessage + "|";
                    }
                }
                //ajaxResult.Message = ajaxResult.Message?.TrimEnd('|');
                throw new ValidatorException(message);
                //context.Result = new JsonResult(ajaxResult);
            }
        }
        /// <summary>
        /// 处理结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}