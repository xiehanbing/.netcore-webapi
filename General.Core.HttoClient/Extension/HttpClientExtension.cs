using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
                WriteClientLog(client.BaseAddress.AbsoluteUri, "Post:" + client.BaseAddress.AbsoluteUri,
                    client.DefaultRequestHeaders.GetSerializeObject() + ";httocintent:" +
                    httpContent.GetSerializeObject(), response.Content.ReadAsStringAsync().Result);
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
            InitRequest(client, httpContent, data, method);
            //if (client.DefaultRequestHeaders?.Contains("isHik") ?? false)
            //{
            //    var formData = data.GetSerializeObject();
            //    client.DefaultRequestHeaders.SetHikSecurityHeaders(formData, client.BaseAddress.AbsoluteUri, data,
            //        method);
            //    httpContent.Headers.ContentMD5 = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(formData));
            //    if (client.DefaultRequestHeaders.Contains("X-Ca-Signature"))
            //    {
            //        var value = client.DefaultRequestHeaders.GetValues("X-Ca-Signature");
            //        httpContent.Headers.Add("X-Ca-Signature", value);
            //        client.DefaultRequestHeaders.Remove("X-Ca-Signature");
            //    }
            //    if (client.DefaultRequestHeaders.Contains("X-Ca-Signature-Headers"))
            //    {
            //        var value = client.DefaultRequestHeaders.GetValues("X-Ca-Signature-Headers");
            //        httpContent.Headers.Add("X-Ca-Signature-Headers", value);
            //        client.DefaultRequestHeaders.Remove("X-Ca-Signature-Headers");
            //    }
            //}
        }

        private static void InitRequest(System.Net.Http.HttpClient client, HttpContent httpContent, object data,
            string method)
        {
            var isPost = method.ToLowerInvariant().Equals("post");
            var url = client.BaseAddress.AbsoluteUri;
            var isHttps = url.Contains("https");
            if (client.DefaultRequestHeaders?.Contains("isHik") ?? false)
            {
                Dictionary<string,string> header=new Dictionary<string, string>();
                header.Add("Accept", GetAccept());
                header.Add("Content-Type",GetContentType());
                if (method.ToLowerInvariant().Equals("post"))
                {
                    header.Add("content-md5",GetContentMd5(data.GetSerializeObject()));
                }
                header.Add("x-ca-timestamp",GetMsTime());

                header.Add("x-ca-nonce", Guid.NewGuid().ToString());

                header.Add("x-ca-key", HikSecurityContext.ArtemisAppKey);
                // build string to sign
                string strToSign = BuildSignString(isPost ? "POST" : "GET", client.BaseAddress.AbsolutePath, header);
                string signedStr = ComputeForHmacsha256(strToSign);

                // x-ca-signature
                header.Add("x-ca-signature", signedStr);
                if (isHttps)
                {
                    ServicePointManager.ServerCertificateValidationCallback=new RemoteCertificateValidationCallback(RemoteCertificateValidate);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                }
                httpContent.Headers.ContentType=new MediaTypeHeaderValue(header["Content-Type"]);
                client.DefaultRequestHeaders.Add("Accept", header["Accept"]);
                client.DefaultRequestHeaders.Add("method", isPost?"POST":"GET");
                foreach (string headerKey in header.Keys)
                {
                    if (headerKey.Contains("x-ca-"))
                    {
                        client.DefaultRequestHeaders.Add(headerKey ,header[headerKey]);
                    }
                }
            }
        }

        private static string BuildSignString(string method, string url, Dictionary<string, string> header)
        {
            StringBuilder sb=new StringBuilder();
            sb.Append(method.ToUpper()).Append("\n");
            if (header != null)
            {
                if (header["Accept"] != null)
                {
                    sb.Append(header["Accept"]).Append("\n");
                }
                if (header.Keys.Contains("Content-MD5") && null != header["Content-MD5"])
                {
                    sb.Append((string)header["Content-MD5"]);
                    sb.Append("\n");
                }

                if (null != header["Content-Type"])
                {
                    sb.Append((string)header["Content-Type"]);
                    sb.Append("\n");
                }

                if (header.Keys.Contains("Date") && null != header["Date"])
                {
                    sb.Append((string)header["Date"]);
                    sb.Append("\n");
                }

            }
            string signHeader = BuildSignHeader(header);
            sb.Append(signHeader);
            sb.Append(url);
            return sb.ToString();
        }
        /// <summary>
        /// 远程证书验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cert"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns>验证是否通过，始终通过</returns>
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
        /// <summary>
        /// 计算签名头
        /// </summary>
        /// <param name="header">请求头</param>
        /// <returns>签名头</returns>
        private static string BuildSignHeader(Dictionary<string, string> header)
        {
            Dictionary<string, string> sortedDicHeader = new Dictionary<string, string>();
            sortedDicHeader = header;
            var dic = from objDic in sortedDicHeader orderby objDic.Key ascending select objDic;
            StringBuilder sbSignHeader = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in dic)
            {
                if (kvp.Key.Replace(" ", "").Contains("x-ca-"))
                {
                    sb.Append(kvp.Key + ":");
                    if (!string.IsNullOrWhiteSpace(kvp.Value))
                    {
                        sb.Append(kvp.Value);
                    }
                    sb.Append("\n");
                    if (sbSignHeader.Length > 0)
                    {
                        sbSignHeader.Append(",");
                    }
                    sbSignHeader.Append(kvp.Key);
                }
            }

            header.Add("x-ca-signature-headers", sbSignHeader.ToString());

            return sb.ToString();
        }
        /// <summary>
        /// 计算HMACSHA265
        /// </summary>
        /// <param name="sign">待计算字符串</param>
        /// <returns>HMAXH265计算结果字符串</returns>
        private static string ComputeForHmacsha256(string sign)
        {
            var encoder = new System.Text.UTF8Encoding();
            byte[] secretBytes = encoder.GetBytes(HikSecurityContext.ArtemisAppSecret);
            byte[] strBytes = encoder.GetBytes(sign);
            var opertor = new HMACSHA256(secretBytes);
            byte[] hashbytes = opertor.ComputeHash(strBytes);
            return Convert.ToBase64String(hashbytes);
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

                WriteClientLog(client.BaseAddress.AbsoluteUri, "Post:" + client.BaseAddress.AbsoluteUri,
                    client.DefaultRequestHeaders.GetSerializeObject() + ";httocintent:" +
                    httpContent.GetSerializeObject(), response.Content.ReadAsStringAsync().Result);
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
            WriteClientLog(client.BaseAddress.AbsoluteUri, "Get:" + client.BaseAddress.AbsoluteUri,
                client.DefaultRequestHeaders.GetSerializeObject(), response.Content.ReadAsStringAsync().Result);
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
            WriteClientLog(client.BaseAddress.AbsoluteUri, "Get:" + client.BaseAddress.AbsoluteUri,
                client.DefaultRequestHeaders.GetSerializeObject(), response.Content.ReadAsStringAsync().Result);
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
            WriteClientLog(client.BaseAddress.AbsoluteUri, "Delete:" + client.BaseAddress.AbsoluteUri,
                client.DefaultRequestHeaders.GetSerializeObject(), response.Content.ReadAsStringAsync().Result);
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
                WriteClientLog(client.BaseAddress.AbsoluteUri, "Put:" + client.BaseAddress.AbsoluteUri,
                    httpContent.GetSerializeObject(), response.Content.ReadAsStringAsync().Result);
              
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
        private static string GetMsTime()
        {
            var timestamp = ((DateTime.Now.Ticks -
                              TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks) /
                             1000).ToString();
            return timestamp;
            //return DateTime.Now.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
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
            MD5 md5=new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash( Encoding.UTF8.GetBytes(body));
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
            return "application/json";
        }
        /// <summary>
        /// 获取 content-type
        /// </summary>
        /// <returns></returns>
        private static string GetContentType()
        {
            return "application/json";
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
            headers.Add("X-Ca-Signature", HmacSha256(sign));
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
        /// <summary>
        /// HmacSHA256
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string HmacSha256(string message)
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
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="keyNo">标示</param>
        /// <param name="modelName">模块名称</param>
        /// <param name="request">请求日志</param>
        /// <param name="response">响应日志</param>
        private static void WriteClientLog(string keyNo, string modelName, string request, string response)
        {
            LogManage.HttpClientLog(new ApiLog()
            {
                ConfirmNo = keyNo,
                ModelName = modelName,
                RequestContext = request,
                ResponseContext = response
            });
        }
    }
}