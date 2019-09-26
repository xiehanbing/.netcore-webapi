using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace General.Api.Framework.Middleware
{
    /// <summary>
    /// 消息处理中间件
    /// </summary>
    public class MessageHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public MessageHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //using (MemoryStream stream= new MemoryStream())
            //{
            //    using (StreamWriter writer = new StreamWriter(stream))
            //    {
            //        writer.Write("123");
            //        writer.Flush();
            //    }
            //    context.Request.Body = stream;
            //}
            await _next.Invoke(context);
        }
    }


    public static class MessageHandlerExtension
    {
        public static IApplicationBuilder UserMessageHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MessageHandlerMiddleware>();
        }
    }
}