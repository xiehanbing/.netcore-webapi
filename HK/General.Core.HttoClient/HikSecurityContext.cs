namespace General.Core.HttpClient
{
    /// <summary>
    /// 海康加密 上下文
    /// </summary>
    public class HikSecurityContext
    {
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
        /// <summary>
        /// artemis网关服务器ip端口
        /// </summary>
        public static string ArtemisHost = "";
        /// <summary>
        /// 秘钥appkey
        /// </summary>
        public static string ArtemisAppKey = "";
        /// <summary>
        /// 秘钥appSecret
        /// </summary>
        public static string ArtemisAppSecret = "";
    }
}