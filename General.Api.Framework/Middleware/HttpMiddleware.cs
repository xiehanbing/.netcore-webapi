using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using General.Api.Core.Log;
using General.Core.Encrypt;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;

namespace General.Api.Framework.Middleware
{
    /// <summary>
    /// HttpHandleMiddleware
    /// </summary>
    public class HttpHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private Stopwatch _stopwatch;
        private readonly ILogger _logger;
        /// <summary>
        /// ErrorHandlingMiddleware
        /// </summary>
        public HttpHandleMiddleware(RequestDelegate next, ILogger<HttpHandleMiddleware> logger)
        {
            this._next = next;
     
            _logger = logger;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            //设置request 可被多次读取
            context.Request.EnableBuffering();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _logger.LogInformation($"Handling request: " + context.Request.Path);
            var logModel = new ApiLog()
            {
                ConfirmNo = context.Request.Path.Value,
                ModelName = context.Request.Method,
            };
            //swagger
            if (!IsEncryRoute(context))
            {
                await _next.Invoke(context);
            }
            else
            {
                var request = context.Request.Body;
                var response = context.Response.Body;
                try
                {
                    using (var newRequest = new MemoryStream())
                    {
                        //替换request 流
                        context.Request.Body = newRequest;
                        using (var newResponse = new MemoryStream())
                        {
                            //替换 response 流
                            context.Response.Body = newResponse;

                            //读取原始消息
                            using (var reader = new StreamReader(request))
                            {
                                //读取原始请求的消息
                                var originMessage = await reader.ReadToEndAsync();
                                logModel.RequestContext = originMessage;
                                if (string.IsNullOrEmpty(originMessage))
                                {
                                    await _next.Invoke(context);
                                }

                                logModel.RequestContext = originMessage.AesDescry();
                            }

                            using (var writer = new StreamWriter(newRequest))
                            {
                                await writer.WriteAsync(logModel.RequestContext);
                                await writer.FlushAsync();
                                newRequest.Position = 0;
                                context.Request.Body = newRequest;
                                await _next(context);
                            }

                            using (var reader = new StreamReader(newResponse))
                            {
                                newResponse.Position = 0;
                                //logModel.ResponseContext = await reader.ReadToEndAsync();
                                var messageResponse = await reader.ReadToEndAsync();
                                if (messageResponse.IsNotWhiteSpace())
                                {
                                    try
                                    {
                                        var serizeObj = messageResponse.GetDeserializeObject<ApiResult<object>>();
                                        var serizeString = string.Empty;
                                        if (serizeObj?.Result != null)
                                        {
                                            if (serizeObj.Result is string)
                                            {
                                                serizeString = (serizeObj.Result).ToString().AesEncry();
                                            }
                                            else
                                            {
                                                serizeString = serizeObj.Result.GetSerializeObject().AesEncry();
                                            }
                                        }
                                        logModel.ResponseContext=(new ApiResult<string>()
                                        {
                                           Success = serizeObj.Success,
                                            Code = serizeObj.Code,
                                            Message = serizeObj.Message,
                                            Result = serizeString
                                        }).GetSerializeObject();
                                    }
                                    catch (Exception e)
                                    {
                                        //不做任何的处理
                                    }
                                }
                                //if (logModel.ResponseContext.IsNotWhiteSpace())
                                //{
                                //    logModel.ResponseContext = logModel.ResponseContext.AesEncry();
                                //}
                            }

                            using (var writer = new StreamWriter(response))
                            {
                                await writer.WriteAsync(logModel.ResponseContext);
                                await writer.FlushAsync();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($" http中间件发生错误: " + ex.ToString());
                }
                finally
                {
                    context.Request.Body = request;
                    context.Response.Body = response;
                }
            }
            //获取原始消息
            var stopTime1 = 0;
            context.Response.OnCompleted(() =>
            {
                _stopwatch.Stop();
                var stopTime = _stopwatch.ElapsedMilliseconds;
                _logger.LogDebug($"RequestLog:{DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next(0, 10000)}-{stopTime}ms", $"{logModel.GetSerializeObject()}");
                return Task.CompletedTask;
            });
            _logger.LogInformation($"Finished handling request.{_stopwatch.ElapsedMilliseconds}ms");


            //context.Response.OnCompleted(async o =>
            //{
            //    _stopwatch.Stop();
            //    if (o is HttpContext c)
            //    {
            //        var requestStr = await c.Request.ReadRequestAsync();
            //        var retStr = await c.Response.ReadBodyAsync();
            //        LogManage.ApiLog(new ApiLog()
            //        {
            //            ConfirmNo = c.Request.Path.Value,
            //            ModelName = c.Request.Method,
            //            RequestContext = requestStr,
            //            ResponseContext = retStr
            //        });
            //    }
            //}, context);
            //await _next(context);


        }

        /// <summary>
        /// 判断是否为 要统一加解密的路由
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool IsEncryRoute(HttpContext context)
        {

            //swagger
            if (context.Request.Path.Value.Contains("getAes") || context.Request.Method.ToLowerInvariant().Equals("get") || context.Request.Path.Value.Contains("swagger"))
            {
                return false;
            }

            return true;
        }

    }

    public static class HttpHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpHandleMiddleware>();
        }
    }
}