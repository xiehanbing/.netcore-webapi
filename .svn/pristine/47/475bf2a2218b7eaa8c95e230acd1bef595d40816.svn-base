using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using General.Core.Extension;

namespace General.Api.Framework.Delegate
{
    /// <summary>
    /// MessageHandler
    /// </summary>
    public class MessageHandler : DelegatingHandler
    {
        /// <summary>
        /// SendAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            //todo request handle
            //此处实现过滤逻辑
            var method = request.Method;
            Console.WriteLine("messageHandle-->method:" + method);
            var url = request.RequestUri.PathAndQuery;
            Console.WriteLine("messageHandle-->url:"+url);
            var response = await base.SendAsync(request, cancellationToken);

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("messageHandle-->response->content:"+content);

            response.Content=new StringContent(new ApiResult(true,"messagehandletest").GetSerializeObject());
            //todo response handle 

            return response;
        }


    }
}