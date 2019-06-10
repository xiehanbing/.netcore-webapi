namespace General.Core.HttpClient
{
    public class HttpClientContext
    {
        /// <summary>
        /// DefaultContentType 
        /// </summary>
        public const string DefaultContentType = "application/json";

        public const int TimeOut = 30;
        //public int TimeOut = 30;
        /// <summary>
        /// 签名Header
        /// </summary>
        public const string XCaSignature = "x-ca-signature";
        /// <summary>
        /// 所有参与签名的Header
        /// </summary>
        public const string XCaSignatureHeaders = "x-ca-signature-headers";
        /// <summary>
        /// 请求时间戳
        /// </summary>
        public const string XCaTimestamp = "x-ca-timestamp";
        /// <summary>
        /// 请求放重放Nonce,15分钟内保持唯一,建议使用UUID
        /// </summary>
        public const string XCaNonce = "x-ca-nonce";
        /// <summary>
        /// APP KEY
        /// </summary>
        public const string XCaKey = "x-ca-key";

        /// <summary>
        /// 请求Header Accept
        /// </summary>
        public const string HttpHeaderAccept = "Accept";
        /// <summary>
        /// 请求Body内容MD5 Header
        /// </summary>
        public const string HttpHeaderContentMd5 = "Content-MD5";
        /// <summary>
        /// 请求Header Content-Type
        /// </summary>
        public const string HttpHeaderContentType = "Content-Type";
        /// <summary>
        /// 请求Header UserAgent
        /// </summary>
        public const string HttpHeaderUserAgent = "User-Agent";
        /// <summary>
        /// 请求Header Date
        /// </summary>
        public const string HttpHeaderDate = "Date";
    }
}