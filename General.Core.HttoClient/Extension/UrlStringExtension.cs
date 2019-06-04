using System;

namespace General.Core.HttpClient.Extension
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
        private static Uri AppendFormatUrl(this string url, string subUrl)
        {
            return new UriBuilder(url.StringCombine(subUrl)).Uri;
        }
        /// <summary>
        /// 获取client baseurl
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="subUrl">子地址</param>
        /// <returns></returns>
        public static System.Net.Http.HttpClient AppendFormat(this string url, string subUrl)
        {
            var client = new System.Net.Http.HttpClient();
            var urlAddress = url.AppendFormatUrl(subUrl);
            client.BaseAddress = urlAddress;
            return client;
        }
    }
}