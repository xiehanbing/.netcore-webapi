using General.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using General.Api.Core.Log;
using General.Core.Extension;
using General.EntityFrameworkCore.Log;
using Microsoft.Extensions.Logging;

namespace HttpUtil
{
    public class HikHttpUtillib : IHikHttpUtillib
    {
        private readonly ILogger<HikHttpUtillib> _logger;
        /// <summary>
        /// 是否使用HTTPS协议
        /// </summary>
        private static bool _isHttps = false;

        public HikHttpUtillib(ILogger<HikHttpUtillib> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 设置信息参数
        /// </summary>
        /// <param name="appkey">合作方APPKey</param>
        /// <param name="secret">合作方APPSecret</param>
        /// <param name="ip">平台IP</param>
        /// <param name="port">平台端口，默认HTTPS的443端口</param>
        /// <param name="isHttps">是否启用HTTPS协议，默认HTTPS</param>
        /// <return></return>
        public static void SetPlatformInfo(string appkey, string secret, string ip, int port = 443, bool isHttps = true)
        {
            _isHttps = isHttps;
        }
        /// <summary>
        /// HTTP GET请求
        /// </summary>
        /// <param name="uri">HTTP接口Url，不带协议和端口，如/artemis/api/resource/v1/cameras/indexCode?cameraIndexCode=a10cafaa777c49a5af92c165c95970e0</param>
        /// <returns></returns>
        public async Task<byte[]> GetByteAsync(string uri)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();

            // 初始化请求：组装请求头，设置远程证书自动验证通过
            InitRequest(header, uri, "", false);

            // build web request object
            StringBuilder sb = new StringBuilder();
            sb.Append(HikSecurityContext.IsHttps ? "https://" : "http://").Append(HikSecurityContext.Ip).Append(":").Append(HikSecurityContext.Port.ToString()).Append(uri);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
            req.KeepAlive = false;
            req.ProtocolVersion = HttpVersion.Version11;
            req.AllowAutoRedirect = false;   // 不允许自动重定向
            req.Method = "GET";
            req.Timeout = HikSecurityContext.TimeOut * 1000;    // 传入是秒，需要转换成毫秒
            req.Accept = header["Accept"];
            req.ContentType = header["Content-Type"];

            foreach (string headerKey in header.Keys)
            {
                if (headerKey.Contains("x-ca-"))
                {
                    req.Headers.Add(headerKey + ":" + header[headerKey]);
                }
            }

            HttpWebResponse rsp = null;
            try
            {
                rsp = (HttpWebResponse)(await req.GetResponseAsync());
                if (HttpStatusCode.OK == rsp.StatusCode)
                {
                    Stream rspStream = rsp.GetResponseStream();     // 响应内容字节流
                    StreamReader sr = new StreamReader(rspStream);
                    string strStream = sr.ReadToEnd();
                    long streamLength = strStream.Length;
                    byte[] response = System.Text.Encoding.UTF8.GetBytes(strStream);
                    rsp.Close();
                    return response;
                }
                else if (HttpStatusCode.Found == rsp.StatusCode || HttpStatusCode.Moved == rsp.StatusCode)  // 302/301 redirect
                {
                    string reqUrl = rsp.Headers["Location"].ToString();   // 获取重定向URL
                    WebRequest wreq = WebRequest.Create(reqUrl);          // 重定向请求对象
                    WebResponse wrsp = wreq.GetResponse();                // 重定向响应
                    long streamLength = wrsp.ContentLength;               // 重定向响应内容长度
                    Stream rspStream = wrsp.GetResponseStream();          // 响应内容字节流
                    byte[] response = new byte[streamLength];
                    rspStream.Read(response, 0, (int)streamLength);       // 读取响应内容至byte数组
                    rspStream.Close();
                    rsp.Close();
                    return response;
                }

                rsp.Close();
            }
            catch (WebException e)
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }

            return null;
        }
        /// <summary>
        /// <see cref="IHikHttpUtillib.GetAsync{T}(string)"/>
        /// </summary>
        public async Task<T> GetAsync<T>(string url)
        {
            var data = await GetByteAsync(url);
            if (data != null)
            {
                var stringData = Encoding.Default.GetString(data);
                return stringData.GetDeserializeObject<T>();

            }
            return default(T);
        }
        /// <summary>
        /// <see cref="IHikHttpUtillib.PostAsync{T}(string,object)"/>
        /// </summary>
        public async Task<T> PostAsync<T>(string url, object body)
        {
            var data = await PostByteAsync(url, body.GetSerializeObject());
            if (data != null)
            {
                var stringData = Encoding.Default.GetString(data);
                return stringData.GetDeserializeObject<T>();
            }
            return default(T);
        }
        /// <summary>
        /// <see cref="IHikHttpUtillib.GetStringAsync(string)"/>
        /// </summary>
        public async Task<string> GetStringAsync(string uri)
        {
            var data = await GetByteAsync(uri);
            if (data != null)
            {
                var stringData = Encoding.Default.GetString(data);
                return stringData;

            }

            return string.Empty;
        }
        /// <summary>
        /// <see cref="IHikHttpUtillib.PostStringAsync(string,object)"/>
        /// </summary>
        public async Task<string> PostStringAsync(string uri, object body)
        {
            var data = await PostByteAsync(uri, body.GetSerializeObject());
            if (data != null)
            {
                var stringData = Encoding.Default.GetString(data);
                return stringData;
            }
            return string.Empty;
        }
        /// <summary>
        /// HTTP Post请求
        /// </summary>
        /// <param name="uri">HTTP接口Url，不带协议和端口，如/artemis/api/resource/v1/org/advance/orgList</param>
        /// <param name="body">请求参数</param>
        /// <return>请求结果</return>
        public async Task<byte[]> PostByteAsync(string uri, string body)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            if (HikSecurityContext.Address.IsNotWhiteSpace())
            {
                //sb.Append("/" + HikSecurityContext.Address);
                uri = "/" + HikSecurityContext.Address + uri;
            }
            // 初始化请求：组装请求头，设置远程证书自动验证通过
            InitRequest(header, uri, body, HikSecurityContext.IsHttps);

            // build web request object
            string compluteUrl = (HikSecurityContext.IsHttps ? "https://" : "http://") + (HikSecurityContext.Ip + ":") +
                                 (HikSecurityContext.Port.ToString()) + uri;
            StringBuilder sbHeader=new StringBuilder();
            // 创建POST请求
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(compluteUrl);
            req.KeepAlive = false;
            req.ProtocolVersion = HttpVersion.Version11;
            req.AllowAutoRedirect = false;   // 不允许自动重定向
            req.Method = "POST";
            req.Timeout = HikSecurityContext.TimeOut * 1000;    // 传入是秒，需要转换成毫秒
            req.Accept = header["Accept"];
            req.ContentType = header["Content-Type"];
            var headersStr = "";
            foreach (string headerKey in header.Keys)
            {
                headersStr += headerKey + ":" + header[headerKey] + "  \r\n  ";
                if (headerKey.Contains("x-ca-"))
                {
                    req.Headers.Add(headerKey + ":" + header[headerKey]);
                }
            }

            foreach (var reqHeader in header)
            {
                sbHeader.Append($"{reqHeader.Key}:{reqHeader.Value}  \n  ");
            }
            if (!string.IsNullOrWhiteSpace(body))
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(body);
                req.ContentLength = postBytes.Length;
                Stream reqStream = null;
                reqStream = req.GetRequestStream();
                reqStream.Write(postBytes, 0, postBytes.Length);
                reqStream.Close();
            }

            HttpWebResponse rsp = null;
            try
            {
                rsp = (HttpWebResponse)(await req.GetResponseAsync());
                if (HttpStatusCode.OK == rsp.StatusCode)
                {
                    Stream rspStream = rsp.GetResponseStream();
                    StreamReader sr = new StreamReader(rspStream);
                    string strStream = sr.ReadToEnd();
                    long streamLength = strStream.Length;
                    byte[] response = System.Text.Encoding.UTF8.GetBytes(strStream);
                    rsp.Close();
                    WriteClientLog(uri, "Post" + compluteUrl, sbHeader+ req.GetSerializeObject()+"; body"+body, Encoding.Default.GetString(response));
                    return response;
                }
                else if (HttpStatusCode.Found == rsp.StatusCode || HttpStatusCode.Moved == rsp.StatusCode)  // 302/301 redirect
                {
                    WriteClientLog(uri, "Post" + compluteUrl, req.GetSerializeObject()+"; header:"+ sbHeader + "; body" + body, rsp.GetSerializeObject());
                }

                rsp.Close();
            }
            catch (Exception ex)
            {
                WriteClientLog(uri, "Post" + compluteUrl, req.GetSerializeObject() + "; header:" + sbHeader + "; body" + body, ex.GetSerializeObject());
            }
           

            return null;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="keyNo">标示</param>
        /// <param name="modelName">模块名称</param>
        /// <param name="request">请求日志</param>
        /// <param name="response">响应日志</param>
        private void WriteClientLog(string keyNo, string modelName, string request, string response)
        {
            LogManage.HttpClientLog(new ApiLog()
            {
                ConfirmNo = keyNo,
                ModelName = modelName,
                RequestContext = request,
                ResponseContext = response
            });
        }
        /// <summary>
        /// 设置请求
        /// </summary>
        /// <param name="header"></param>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="isPost"></param>
        private void InitRequest(Dictionary<string, string> header, string url, string body, bool isPost)
        {
            // Accept                
            string accept = "application/json";// "*/*";
            header.Add("Accept", accept);

            // ContentType  
            string contentType = "application/json";
            header.Add("Content-Type", contentType);

            if (isPost)
            {
                // content-md5，be careful it must be lower case.
                string contentMd5 = computeContentMd5(body);
                header.Add("content-md5", contentMd5);
            }

            // x-ca-timestamp
            string timestamp = ((DateTime.Now.Ticks - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks) / 1000).ToString();
            header.Add("x-ca-timestamp", timestamp);

            // x-ca-nonce
            string nonce = System.Guid.NewGuid().ToString();
            header.Add("x-ca-nonce", nonce);

            // x-ca-key
            header.Add("x-ca-key", HikSecurityContext.ArtemisAppKey);

            // build string to sign
            string strToSign = buildSignString(isPost ? "POST" : "GET", url, header);
            string signedStr = computeForHMACSHA256(strToSign, HikSecurityContext.ArtemisAppSecret);

            // x-ca-signature
            header.Add("x-ca-signature", signedStr);

            if (_isHttps)
            {
                // set remote certificate Validation auto pass
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(remoteCertificateValidate);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            }
        }

        /// <summary>
        /// 计算content-md5
        /// </summary>
        /// <param name="body"></param>
        /// <returns>base64后的content-md5</returns>
        private string computeContentMd5(string body)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(body));
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 远程证书验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cert"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns>验证是否通过，始终通过</returns>
        private bool remoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        /// <summary>
        /// 计算HMACSHA265
        /// </summary>
        /// <param name="str">待计算字符串</param>
        /// <param name="secret">平台APPSecet</param>
        /// <returns>HMAXH265计算结果字符串</returns>
        private string computeForHMACSHA256(string str, string secret)
        {
            var encoder = new System.Text.UTF8Encoding();
            byte[] secretBytes = encoder.GetBytes(secret);
            byte[] strBytes = encoder.GetBytes(str);
            var opertor = new HMACSHA256(secretBytes);
            byte[] hashbytes = opertor.ComputeHash(strBytes);
            return Convert.ToBase64String(hashbytes);
        }

        /// <summary>
        /// 计算签名字符串
        /// </summary>
        /// <param name="method">HTTP请求方法，如“POST”</param>
        /// <param name="url">接口Url，如/artemis/api/resource/v1/org/advance/orgList</param>
        /// <param name="header">请求头</param>
        /// <returns>签名字符串</returns>
        private string buildSignString(string method, string url, Dictionary<string, string> header)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(method.ToUpper()).Append("\n");
            if (null != header)
            {
                if (null != header["Accept"])
                {
                    sb.Append((string)header["Accept"]);
                    sb.Append("\n");
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

            // build and add header to sign
            string signHeader = buildSignHeader(header);
            sb.Append(signHeader);
            sb.Append(url);
            return sb.ToString();
        }

        /// <summary>
        /// 计算签名头
        /// </summary>
        /// <param name="header">请求头</param>
        /// <returns>签名头</returns>
        private string buildSignHeader(Dictionary<string, string> header)
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

    }
}
