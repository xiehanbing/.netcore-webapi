using System;

namespace General.HttpClient.Extension
{
    /// <summary>
    /// url 扩展类
    /// </summary>
    public static class UrlStringExtension
    {
        /// <summary>
        /// url 拼接
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="subUrl">子地址</param>
        /// <returns>最终的url</returns>
        public static Uri AppendFormat(this string url, string subUrl)
        {
            return new UriBuilder(url.StringCombine(subUrl)).Uri;
        }
    }
}