using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
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
                var clientDefaultHeaders = client.DefaultRequestHeaders?
                                               .FirstOrDefault(o => o.Key.ToLower().Equals("content-type"))
                                               .Value?.FirstOrDefault() ?? "";
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

                }

                SetHiSecuritySignHeader(client, httpContent, data, "post");

                //if (client.DefaultRequestHeaders?.Any() ?? false)
                //{
                //    foreach (var item in client.DefaultRequestHeaders)
                //    {
                //        httpContent.Headers.Add(item.Key, item.Value);
                //    }
                //}
                var response = await client.PostAsync(client.BaseAddress, httpContent);
                LogManage.HttpClientLog(new HttpClientApiLog()
                {
                    ConfirmNo = client.BaseAddress.AbsoluteUri,
                    ModelName = "Post:" + client.BaseAddress.AbsoluteUri,
                    RequestContext = client.DefaultRequestHeaders.GetSerializeObject() + ";httocintent:" + httpContent.GetSerializeObject(),
                    ResponseContext = response.Content.ReadAsStringAsync().Result
                });
                client.Dispose();
                return response;
            }
        }
        /// <summary>
        /// 设置最终访问的 header 数据
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="httpContent">httpContent</param>
        /// <param name="data">data</param>
        /// <param name="method">method</param>
        private static void SetHiSecuritySignHeader(System.Net.Http.HttpClient client, HttpContent httpContent, object data, string method)
        {
            if (client.DefaultRequestHeaders?.Contains("isHik") ?? false)
            {
                var formData = data.GetSerializeObject();
                client.DefaultRequestHeaders.SetHikSecurityHeaders(formData, client.BaseAddress.AbsoluteUri, data,
                    method);
                httpContent.Headers.ContentMD5 = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(formData));
                if (client.DefaultRequestHeaders.Contains("X-Ca-Signature"))
                {
                    var value = client.DefaultRequestHeaders.GetValues("X-Ca-Signature");
                    httpContent.Headers.Add("X-Ca-Signature", value);
                    client.DefaultRequestHeaders.Remove("X-Ca-Signature");
                }
                if (client.DefaultRequestHeaders.Contains("X-Ca-Signature-Headers"))
                {
                    var value = client.DefaultRequestHeaders.GetValues("X-Ca-Signature-Headers");
                    httpContent.Headers.Add("X-Ca-Signature-Headers", value);
                    client.DefaultRequestHeaders.Remove("X-Ca-Signature-Headers");
                }
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
                SetHiSecuritySignHeader(client, httpContent, data, "post");
                var response = client.PostAsync(client.BaseAddress, httpContent).Result;
                LogManage.HttpClientLog(new HttpClientApiLog()
                {
                    ConfirmNo = client.BaseAddress.AbsoluteUri,
                    ModelName = "Post:" + client.BaseAddress.AbsoluteUri,
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
            if (client.DefaultRequestHeaders?.Contains("isHik") ?? false)
            {
                client.DefaultRequestHeaders.SetHikSecurityHeaders(null, client.BaseAddress.AbsoluteUri, null,
                    "get");
            }
            var response = client.GetAsync(client.BaseAddress).Result;

            LogManage.HttpClientLog(new HttpClientApiLog()
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
        /// get 
        /// </summary>
        /// <param name="client">httpclient </param>
        /// <returns>httpcontext</returns>
        public static async Task<HttpContent> GetAsync(this System.Net.Http.HttpClient client)
        {
            if (client.DefaultRequestHeaders?.Contains("isHik") ?? false)
            {
                client.DefaultRequestHeaders.SetHikSecurityHeaders(null, client.BaseAddress.AbsoluteUri, null,
                    "get");
            }
            var response = await client.GetAsync(client.BaseAddress);

            LogManage.HttpClientLog(new HttpClientApiLog()
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
            if (client.DefaultRequestHeaders?.Contains("isHik") ?? false)
            {
                client.DefaultRequestHeaders.SetHikSecurityHeaders(null, client.BaseAddress.AbsoluteUri, null,
                    "delete");
            }
            var response = await client.DeleteAsync(client.BaseAddress);
            LogManage.HttpClientLog(new HttpClientApiLog()
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
                SetHiSecuritySignHeader(client, httpContent, data, "put");
                var response = await client.PutAsync(client.BaseAddress, httpContent);
                LogManage.HttpClientLog(new HttpClientApiLog()
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
        /// 设置海康秘钥等相关信息
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static System.Net.Http.HttpClient SetHiKSecreity(this System.Net.Http.HttpClient client)
        {
            client.DefaultRequestHeaders.Add("isHik", new List<string>() { "1" });
            return client;
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

        private static string BuildStringToSign(Dictionary<string, string> headers, Dictionary<string, string> securityHeaders)
        {
            StringBuilder signStr = new StringBuilder(100);
            //todo BuildStringToSign
            foreach (var header in headers)
            {
                if (header.Key.Equals("url"))
                {
                    continue;
                }
                signStr.Append($@"{header.Value} \n ");
            }

            foreach (var securityHeader in securityHeaders)
            {
                signStr.Append($@"{securityHeader.Key}:{securityHeader.Value} \n ");
            }

            if (headers.TryGetValue("url", out string urlPath))
            {
                signStr.Append($@"{urlPath} \n ");
            }
            Console.WriteLine("sigStr:" + signStr.ToString());
            return signStr.ToString();
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
            //url query
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

            var formParamMapOrder = formParamMap.OrderBy(o => o.Key).ToList();
            //request body
            if (formParamMapOrder.Any())
            {
                if (!sb.ToString().Trim().IsNotWhiteSpace())
                {
                    if (url.Contains("artemis"))
                    {
                        var value = url.Substring(url.IndexOf("artemis", StringComparison.Ordinal));
                        sb.Append("/" + value);
                    }

                }
                sb.Append("?");
                int flag = 0;
                var count = formParamMap.Count;
                foreach (var item in formParamMapOrder)
                {
                    //if(flag>count)break;
                    if (flag != 0)
                    {
                        sb.Append("&");
                    }

                    flag++;
                    if (item.Key.IsNotNull())
                    {
                        sb.Append($"{item.Key}={item.Value}");
                    }
                }
            }
            Console.WriteLine("BuildResource:"+sb.ToString());
            //todo BuildResource
            return sb.ToString();
        }

        private static string BuildHeader()
        {
            //todo BuildHeader
            return "";
        }
        /// <summary>
        /// 获取 httpmethod 返回的是大写
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static string GetHttpMethod(string method)
        {
            return method?.ToUpper();
        }
        /// <summary>
        /// 获取ContentMd5  utf8 字符集 base64 编码
        /// </summary>
        /// <param name="body">数据</param>
        /// <returns></returns>
        private static string GetContentMd5(string body)
        {
            if (body == null) return "";
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        private static string GetDateString()
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 获取 accept
        /// </summary>
        /// <returns></returns>
        private static string GetAccept()
        {
            return "*/*";
        }
        /// <summary>
        /// 获取 content-type
        /// </summary>
        /// <returns></returns>
        private static string GetContentType()
        {
            return "text/json";
        }
        /// <summary>
        /// 获取headers
        /// </summary>
        /// <param name="headers">headers</param>
        /// <returns></returns>
        private static Dictionary<string, string> GetHeaders(HttpHeaders headers)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var header in headers)
            {
                dic.Add(header.Key.ToLowerInvariant(), header.Value?.FirstOrDefault() ?? "");
            }

            if (!dic.ContainsKey("Date"))
            {
                dic.Add("Date", GetDateString());
            }
            return dic;
        }
        /// <summary>
        /// 获取海康加密的headers
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetHikSecurityHeaders(HttpRequestHeaders headers, string method, string url, Dictionary<string, string> formData)
        {
            method = method.ToUpperInvariant();
            Dictionary<string, string> dic = new Dictionary<string, string> { { "method", method } };
            if (headers.Contains("accept"))
            {
                dic.Add("accept", headers.GetValues("accept-header")?.FirstOrDefault()?.Trim() ?? "");
            }
            if (headers.Any(o => o.Key.ToLower().Equals("content-type")))
            {
                dic.Add("content-type", headers.GetValues("content-type")?.FirstOrDefault()?.Trim() ?? "");
            }
            if (headers.Any(o => o.Key.ToLower().Equals("date")))
            {
                dic.Add("date", headers.GetValues("date")?.FirstOrDefault()?.Trim() ?? "");
            }
            if (headers.Any(o => o.Key.ToLower().Equals("headers")))
            {
                dic.Add("headers", headers.GetValues("headers")?.FirstOrDefault()?.Trim() ?? "");
            }
            dic.Add("url", BuildResource(url, headers, formData));

            return dic;
        }

        /// <summary>
        /// SetHikSecurityHeaders
        /// </summary>
        /// <param name="headers">headers</param>
        /// <param name="body">body</param>
        /// <param name="url">url</param>
        /// <param name="data">data</param>
        /// <param name="method">method</param>
        /// <returns></returns>
        public static HttpRequestHeaders SetHikSecurityHeaders(this HttpRequestHeaders headers, string body, string url, object data, string method)
        {
            var formData = new Dictionary<string, string>();
            if (data != null)
            {
                Type t = data.GetType();

                PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo p in pi)
                {
                    MethodInfo mi = p.GetGetMethod();

                    if (mi != null && mi.IsPublic)
                    {
                        formData.Add(p.Name, mi.Invoke(data, new Object[] { })?.ToString());
                    }
                }
            }
            headers.Date = DateTimeOffset.Now;
            var securityHeaders = GetHikSecurityHeaders(headers, method, url, formData);
            var allHeaders = new Dictionary<string, string>();
            foreach (var header in headers)
            {
                if (header.Value?.Count() > 0)
                {
                    if (header.Key.Equals("url"))
                    {
                        continue;
                    }
                    allHeaders.Add(header.Key, header.Value?.FirstOrDefault() ?? "");
                }
            }
            var sign = BuildStringToSign(securityHeaders, allHeaders);
            headers.Add("X-Ca-Signature", HmacSHA256(sign));
            if (securityHeaders.Count > 0)
            {
                string secheaders = "";
                foreach (var securityHeader in securityHeaders)
                {
                    secheaders += $@"{securityHeader.Key}:{securityHeader.Value} ,";
                }
                headers.Add("X-Ca-Signature-Headers", secheaders.TrimEnd(','));
            }
            headers.Add("appKey", new List<string>() { HikSecurityContext.ArtemisAppKey });
            headers.Add("appSecret", new List<string>() { HikSecurityContext.ArtemisAppSecret });
            return headers;
        }

        private static string HmacSHA256(string message)
        {
            var secret = HikSecurityContext.ArtemisAppSecret ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
    }
}