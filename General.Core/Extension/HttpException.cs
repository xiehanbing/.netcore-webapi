using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace General.Core.Extension
{
    /// <summary>
    /// http 扩展
    /// </summary>
    public static class HttpException
    {
        /// <summary>
        /// 处理request
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        public static async Task<string> ReadRequestAsync(this HttpRequest request)
        {
            var cotent = string.Empty;
            if (request.Path.Value?.IndexOf("?") >= 0)
            {
                cotent = "Uri:" + request.Path.Value.Substring(request.Path.Value.IndexOf("?", StringComparison.Ordinal));
            }

            if (request.Body != null)
            {
                request.Body.Position = 0;
                var encoding = GetEncoding(request.ContentType);
                using (var requestBody = new MemoryStream())
                {
                    request.Body.CopyTo(requestBody);
                    requestBody.Position = 0;
                    var body = await ReadStreamAsync(requestBody, encoding);
                    if (body.IsNotWhiteSpace())
                        cotent += ";Body:" + body;
                    requestBody.Position = 0;
                    request.Body = requestBody;
                }
                //var encoding = GetEncoding(request.ContentType);
                //var body = await ReadStreamAsync(request.Body, encoding);
                //cotent += ";Body:" + body;
            }

            return cotent;
        }

        public static string ReadRequest(this HttpRequest request)
        {
            var cotent = string.Empty;

            if (request.Path.Value != null)
            {
                cotent = "Uri:" + request.Path.Value + ((request.Query?.Any() ?? false) ? ("?" + string.Join("&", request.Query?.Select(o => o.Key + "=" + o.Value))) : "");
            }

            if (request.Body != null)
            {
                request.Body.Position = 0;
                var encoding = GetEncoding(request.ContentType);
                using (var requestBody = new MemoryStream())
                {
                    request.Body.CopyTo(requestBody);
                    requestBody.Position = 0;
                    var body = ReadStream(requestBody, encoding);
                    if (body.IsNotWhiteSpace())
                        cotent += ";Body:" + body;
                    requestBody.Position = 0;
                    //request.Body = requestBody;
                }
            }

            return cotent;
        }
        /// <summary>
        /// 读取 response 相关信息
        /// </summary>
        /// <param name="response">response</param>
        /// <returns></returns>
        public static async Task<string> ReadBodyAsync(this HttpResponse response)
        {
            if (response.Body.Length > 0)
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var encoding = GetEncoding(response.ContentType);
                var retStr = await ReadStreamAsync(response.Body, encoding, false);
                //读取完成后再重新赋值位置这个过程可能不需要，因为数据流是只写的
                return retStr;
            }
            return null;
        }
        /// <summary>
        /// 获取Encoding
        /// </summary>
        /// <param name="contentType">Encodingtype</param>
        /// <returns></returns>
        private static Encoding GetEncoding(string contentType)
        {
            var mediaType = contentType == null ? default(MediaType) : new MediaType(contentType);
            var encoding = mediaType.Encoding;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding;
        }
        /// <summary>
        /// 读取文本
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <param name="forceSeekBeginZero"></param>
        /// <returns></returns>
        private static async Task<string> ReadStreamAsync(Stream stream, Encoding encoding, bool forceSeekBeginZero = true)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))//这里注意Body部分不能随StreamReader一起释放
            {
                var str = await sr.ReadToEndAsync();
                if (forceSeekBeginZero && stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);//内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
                }
                return str;
            }
        }

        private static string ReadStream(Stream stream, Encoding encoding, bool forceSeekBeginZero = true)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))//这里注意Body部分不能随StreamReader一起释放
            {
                var str = sr.ReadToEnd();
                if (forceSeekBeginZero && stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);//内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
                }
                return str;
            }
        }
    }
}