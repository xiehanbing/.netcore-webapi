using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace General.Core.HttpClient.Extension
{
    public static class HttpClientExtension
    {
        /// <summary>
        /// HttpPostAsync 
        /// </summary>
        /// <param name="client">httpclient</param>
        /// <param name="data">data</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(this System.Net.Http.HttpClient client, object data)
        {
            var postData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
            {
                var clientDefaultHeaders = client.DefaultRequestHeaders
                                               .FirstOrDefault(o => o.Key.ToLower().Equals("content-type"))
                                               .Value.FirstOrDefault() ?? "";
                if (!string.IsNullOrEmpty(clientDefaultHeaders))
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(clientDefaultHeaders); ;

                }
                else
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(HttpClientContext.DefaultContentType); ;
                }

                var response = await client.PostAsync(client.BaseAddress, httpContent);

                client.Dispose();
                return response;
            }

        }
        /// <summary>
        /// HttpPost
        /// </summary>
        /// <param name="client"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HttpResponseMessage Post(this System.Net.Http.HttpClient client, object data)
        {
            var postData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
            {
                var clientDefaultHeaders = client.DefaultRequestHeaders
                                               .FirstOrDefault(o => o.Key.ToLower().Equals("content-type"))
                                               .Value.FirstOrDefault() ?? "";
                if (!string.IsNullOrEmpty(clientDefaultHeaders))
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(clientDefaultHeaders); ;

                }
                else
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(HttpClientContext.DefaultContentType); ;
                }
                var response = client.PostAsync(client.BaseAddress, httpContent).Result;

                client.Dispose();
                return response;
            }
        }
        /// <summary>
        /// 添加单个参数 get 请求 或者delete 请求使用
        /// </summary>
        /// <param name="client">httpclient</param>
        /// <param name="key">key</param>
        /// <param name="data">data</param>
        /// <returns>httpclient</returns>
        public static System.Net.Http.HttpClient SetParam(this System.Net.Http.HttpClient client, string key, object data)
        {
            client.DefaultRequestHeaders.Add(key, data.ToString());
            return client;
        }
        /// <summary>
        /// 设置参数 post or put 
        /// </summary>
        /// <param name="client">httpclient</param>
        /// <param name="data">data</param>
        /// <returns>httpclient </returns>
        public static System.Net.Http.HttpClient SetParams(this System.Net.Http.HttpClient client, object data)
        {
            Type type = data.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                string name = propertyInfo.Name;
                object value = propertyInfo.GetValue(data, null);
                client.DefaultRequestHeaders.Add(name, value.ToString());
            }
            return client;
        }
        /// <summary>
        /// set header 
        /// </summary>
        /// <param name="client">httpclient</param>
        /// <param name="headers">header</param>
        /// <returns></returns>
        public static System.Net.Http.HttpClient SetHeaders(this System.Net.Http.HttpClient client,
            Dictionary<string, object> headers)
        {
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
            }

            return client;
        }
        /// <summary>
        /// set timeout 
        /// </summary>
        /// <param name="client">httpclient</param>
        /// <param name="timeOut">timeOut </param>
        /// <returns></returns>
        public static System.Net.Http.HttpClient SetTimeOut(this System.Net.Http.HttpClient client, int timeOut)
        {
            client.Timeout = new TimeSpan(0, 0, timeOut);
            return client;
        }
        /// <summary>
        /// get 
        /// </summary>
        /// <param name="client">httpclient </param>
        /// <returns>httpcontext</returns>
        public static HttpContent Get(this System.Net.Http.HttpClient client)
        {
            var response = client.GetAsync(client.BaseAddress).Result;
            client.Dispose();
            return response.Content;
        }
        /// <summary>
        /// get 
        /// </summary>
        /// <param name="client">httpclient </param>
        /// <returns>httpcontext</returns>
        public static async Task<HttpContent> GetAsync(this System.Net.Http.HttpClient client)
        {
            var response = await client.GetAsync(client.BaseAddress);
            client.Dispose();
            return response.Content;
        }

    }
}