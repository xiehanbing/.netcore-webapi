using System;
using System.IO;
using System.Text;
using General.Api.Core.Log;
using General.Core;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Api.Framework.Filters
{
    /// <summary>
    /// ActionFilter
    /// </summary>
    public class ActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// OnResultExecuting
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Request.EnableRewind();
            var requestStr = context.HttpContext.Request.ReadRequest();
            var path = context.HttpContext.Request.Path.Value;
            if (context.Result is BadRequestObjectResult jsonResult)
            {
                //不做处理
            }
            else if (context.Result is ObjectResult objectResult)
            {
                context.Result = objectResult.Value == null ? new ObjectResult(new ApiResult { Code = 404, Message = "未找到资源" }) : 
                    new ObjectResult(new ApiResult<object> { Code = objectResult.StatusCode??0, Message = "", Result = objectResult.Value, Success = true });
            }
            else if (context.Result is ContentResult contentResult)
            {
                context.Result = new ObjectResult(
                    new ApiResult<object>()
                    {
                        Code = contentResult.StatusCode??0,
                        Message = "",
                        Result = contentResult.Content,
                        Success = true
                    });
            }
            else if (context.Result is StatusCodeResult statusCodeResult)
            {
                context.Result = new ObjectResult(
                    new ApiResult()
                    {
                        Code = statusCodeResult.StatusCode,
                        Message = ""
                    });
            }

            var response = context.Result.GetSerializeObject();
            LogManage.ResourceLog(new ApiLog()
            {
                ConfirmNo = path,
                ModelName = ApiConsts.ProjectName + path,
                RequestContext = requestStr,
                ResponseContext = response
            });
        }
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            //var requestbody = filterContext.HttpContext.Request?.Body;
            //处理body
            //if (requestbody != null)
            //{
            //    using (StreamReader sr = new StreamReader(requestbody, Encoding.UTF8, true, 1024, true)
            //    ) //这里注意Body部分不能随StreamReader一起释放
            //    {
            //        var str = sr.ReadToEnd();
            //        byte[] bytes = new byte[requestbody.Length];
            //        requestbody.Read(bytes, 0, bytes.Length);
            //        if (requestbody.CanSeek)
            //        {
            //            requestbody.Seek(0, SeekOrigin.Begin); //内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
            //        }

            //        filterContext.HttpContext.Request.Body=;
            //    }
            //}
            //string test = "123";
            //byte[] bytes = System.Text.Encoding.Default.GetBytes(test);
            //filterContext.HttpContext.Request.Body.Position = 0;
            //var memery = new MemoryStream();


            //filterContext.HttpContext.Request.Body.CopyToAsync(memery);
            //memery.Position = 0;
            //using (StreamReader reader = new StreamReader(memery, Encoding.UTF8, true, 1024, true))
            //{
            //    var sss = reader.ReadToEnd();
            //    memery.Seek(0, SeekOrigin.Begin);

            //}

            //MemoryStream stream = new MemoryStream(bytes);
            //memery= stream;
            //memery.Position = 0;
            //// memery.
            //filterContext.HttpContext.Request.Body = memery;

            //using (StreamReader reader = new StreamReader(requestBodyStram, Encoding.UTF8, true, 1024, true))
            //{
            //    var sss = reader.ReadToEnd();
            //    requestBodyStram.Seek(0, SeekOrigin.Begin);
            //    requestBodyStram.Write(bytes);

            //}
            //using (StreamReader reader = new StreamReader(requestBodyStram, Encoding.UTF8, true, 1024, true))
            //{
            //    var sss1 = reader.ReadToEnd();
            //    requestBodyStram.Seek(0, SeekOrigin.Begin);

            //}


            ////设置当前流的位置未0
            //requestBodyStram.Seek(0, SeekOrigin.Begin);

            //filterContext.HttpContext.Request.Body=requestBodyStram;
            //using (StreamReader reader = new StreamReader(filterContext.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
            //{
            //    var sss1 = reader.ReadToEnd();
            //    //requestBodyStram.Seek(0, SeekOrigin.Begin);

            //}
            //Console.WriteLine("OnActionExecuting:" + filterContext.HttpContext.Request.Body.GetSerializeObject());
            ////filterContext.Result=new ObjectResult(1);
            //todo request 
            //((BaseRequest)filterContext.ActionArguments["req"]).Operator = 99;
            base.OnActionExecuting(filterContext);
            //if()
        }
    }
}