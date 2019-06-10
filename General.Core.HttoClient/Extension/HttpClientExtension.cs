using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using General.Api.Core.Log;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
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
                    httpContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(clientDefaultHeaders);
                    ;

                }
                else
                {
                    httpContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(HttpClientContext.DefaultContentType);
                    ;
                }

                if (client.DefaultRequestHeaders.Any())
                {
                    foreach (var item in client.DefaultRequestHeaders)
                    {
                        httpContent.Headers.Add(item.Key, item.Value);
                    }
                }


                var response = await client.PostAsync(client.BaseAddress, httpContent);
                LogManage.ApiLog(new ApiLog()
                {
                    ConfirmNo = client.BaseAddress.AbsoluteUri,
                    ModelName = "Post:"+ client.BaseAddress.AbsoluteUri,
                    RequestContext = httpContent.GetSerializeObject(),
                    ResponseContext = response.Content.ReadAsStringAsync().Result
                });
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
                if (client.DefaultRequestHeaders.Any())
                {
                    foreach (var item in client.DefaultRequestHeaders)
                    {
                        httpContent.Headers.Add(item.Key, item.Value);
                    }
                }
                var response = client.PostAsync(client.BaseAddress, httpContent).Result;
                LogManage.ApiLog(new ApiLog()
                {
                    ConfirmNo = client.BaseAddress.AbsoluteUri,
                    ModelName = "Post:"+ client.BaseAddress.AbsoluteUri,
                    RequestContext = httpContent.GetSerializeObject(),
                    ResponseContext = response.Content.ReadAsStringAsync().Result
                });
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
            var baseUrl = client.BaseAddress.AbsoluteUri;
            if (baseUrl.IndexOf("?", StringComparison.Ordinal) >= 0)
            {
                baseUrl += $"{key}={data}";
            }
            else
            {
                baseUrl += $"?{key}={data}";
            }
            client.BaseAddress = new UriBuilder(baseUrl).Uri;
            //client.DefaultRequestHeaders.Add(key, data.ToString());
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
            var baseUrl = client.BaseAddress.AbsoluteUri;
            Type type = data.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                string name = propertyInfo.Name;
                object value = propertyInfo.GetValue(data, null);
                if (baseUrl.IndexOf("?", StringComparison.Ordinal) >= 0)
                {
                    baseUrl += $"{name}={value}";
                }
                else
                {
                    baseUrl += $"?{name}={value}";
                }
                //client.DefaultRequestHeaders.Add(name, value.ToString());
            }
            client.BaseAddress = new UriBuilder(baseUrl).Uri;
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

            LogManage.ApiLog(new ApiLog()
            {
                ConfirmNo = client.BaseAddress.AbsoluteUri,
                ModelName = "Get:"+ client.BaseAddress.AbsoluteUri,
                RequestContext = client.DefaultRequestHeaders.GetSerializeObject(),
                ResponseContext = response.Content.ReadAsStringAsync().Result
            });
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

            LogManage.ApiLog(new ApiLog()
            {
                ConfirmNo = client.BaseAddress.AbsoluteUri,
                ModelName = "Get:" + client.BaseAddress.AbsoluteUri,
                RequestContext = client.DefaultRequestHeaders.GetSerializeObject(),
                ResponseContext = response.Content.ReadAsStringAsync().Result
            });
            client.Dispose();
            return response.Content;
        }
        /// <summary>
        /// delete async
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<HttpContent> DeleteAsync(this System.Net.Http.HttpClient client)
        {
            var response = await client.DeleteAsync(client.BaseAddress);
            LogManage.ApiLog(new ApiLog()
            {
                ConfirmNo = client.BaseAddress.AbsoluteUri,
                ModelName = "Delete:" + client.BaseAddress.AbsoluteUri,
                RequestContext = client.DefaultRequestHeaders.GetSerializeObject(),
                ResponseContext = response.Content.ReadAsStringAsync().Result
            });
            client.Dispose();
            return response.Content;
        }

        public static async Task<HttpResponseMessage> PutAsync(this System.Net.Http.HttpClient client, object data)
        {
            var putData = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            using (HttpContent httpContent = new StringContent(putData, Encoding.UTF8))
            {
                var clientDefaultHeaders = client.DefaultRequestHeaders
                                               .FirstOrDefault(o => o.Key.ToLower().Equals("content-type"))
                                               .Value.FirstOrDefault() ?? "";
                if (!string.IsNullOrEmpty(clientDefaultHeaders))
                {
                    httpContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(clientDefaultHeaders);
                    ;

                }
                else
                {
                    httpContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(HttpClientContext.DefaultContentType);
                    ;
                }

                if (client.DefaultRequestHeaders.Any())
                {
                    foreach (var item in client.DefaultRequestHeaders)
                    {
                        httpContent.Headers.Add(item.Key, item.Value);
                    }
                }


                var response = await client.PutAsync(client.BaseAddress, httpContent);
                LogManage.ApiLog(new ApiLog()
                {
                    ConfirmNo = client.BaseAddress.AbsoluteUri,
                    ModelName = "Put:" + client.BaseAddress.AbsoluteUri,
                    RequestContext = httpContent.GetSerializeObject(),
                    ResponseContext = response.Content.ReadAsStringAsync().Result
                });
                client.Dispose();
                return response;
            }
        }

        /// <summary>
        /// 获取自1970年1月1日以来的毫秒数
        /// </summary>
        /// <returns></returns>
        private static double GetMsTime()
        {
            return DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
        }

        /// <summary>
        /// 请求放重放Nonce,15分钟内保持唯一,建议使用UUID
        /// </summary>
        /// <returns></returns>
        private static string GetUuid()
        {
            //todo create uuid
            return "";
        }

        private static string BuildStringToSign(HttpRequestHeaders headers, HttpMethod method, string url,
            object formParamMap)
        {
            //todo BuildStringToSign
            StringBuilder strToSign = new StringBuilder(200);
            strToSign.Append(method.Method.ToUpper());
            strToSign.Append(@"\n");
            var count = headers.Count() - 1;
            for (int i = 0; i < headers.Count(); i++)
            {
                strToSign.Append()
                if (i != count)
                {

                }
            }
            return "";
        }
        /// <summary>
        /// BuildResource
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="headers">headers</param>
        /// <param name="formParamMap">formParamMap</param>
        /// <returns></returns>
        private static string BuildResource(string url, HttpRequestHeaders headers, Dictionary<string, string> formParamMap)
        {
            StringBuilder sb = new StringBuilder(200);
            if (url.IndexOf("?", StringComparison.Ordinal) >= 0)
            {
                var path = url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
                var queryString = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
                if (queryString.IsNotWhiteSpace())
                {
                    var array = queryString.Split("&");
                    foreach (var item in array)
                    {
                        var itemValue = item.Split("=");
                        if (itemValue.Length >= 2)
                        {
                            var key = itemValue[0];
                            var value = itemValue[1];
                            formParamMap.Add(key, value);
                        }
                    }
                }

                sb.Append(path);
            }

            if (formParamMap.Count > 0)
            {
                sb.Append("?");
                int flag = 0;
                foreach (var item in formParamMap)
                {
                    if (flag != 0)
                    {
                        sb.Append("&");
                    }

                    flag++;
                    if (item.Key.IsNotNull() && item.Value.IsNotNull())
                    {
                        sb.Append($"{item.Key}={item.Value}");
                    }
                }
            }
            //todo BuildResource
            return sb.ToString();
        }

        private static string BuildHeader()
        {
            //todo BuildHeader
            return "";
        }
    }
}