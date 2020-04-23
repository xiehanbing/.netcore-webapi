using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Resilience
{
    /// <summary>
    /// 封装的 httpclient service
    /// </summary>
    public interface IHttpClient
    {
        /// <summary>
        /// post 请求 
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="url">路径</param>
        /// <param name="item">参数</param>
        /// <param name="authorizationToken">author token</param>
        /// <param name="requestId">请求id</param>
        /// <param name="authorizationMethod">请求方式</param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, object item, string authorizationToken = null, string requestId = null,
            string authorizationMethod = "Bearer");
        /// <summary>
        /// post 请求 
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="url">路径</param>
        /// <param name="item">参数</param>
        /// <param name="authorizationToken">author token</param>
        /// <param name="requestId">请求id</param>
        /// <param name="authorizationMethod">请求方式</param>
        /// <returns></returns>
        Task<string> PostAsync(string url, object item, string authorizationToken=null, string requestId = null,
            string authorizationMethod = "Bearer");
        /// <summary>
        /// post 请求 
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="form">参数</param>
        /// <param name="authorizationToken">author token</param>
        /// <param name="requestId">请求id</param>
        /// <param name="authorizationMethod">请求方式</param>
        /// <returns></returns>
        Task<string> PostAsync(string url, Dictionary<string, string> form, string authorizationToken = null, string requestId = null,
            string authorizationMethod = "Bearer");
        /// <summary>
        /// post 请求 
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="form">参数</param>
        /// <param name="authorizationToken">author token</param>
        /// <param name="requestId">请求id</param>
        /// <param name="authorizationMethod">请求方式</param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, Dictionary<string, string> form, string authorizationToken,
            string requestId = null,
            string authorizationMethod = "Bearer");
        /// <summary>
        /// get 请求 
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="url">路径</param>
        /// <param name="authorizationToken">author token</param>
        /// <param name="requestId">请求id</param>
        /// <param name="authorizationMethod">请求方式</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string url, string authorizationToken = null, string requestId = null,
            string authorizationMethod = "Bearer");
        /// <summary>
        /// get 请求 
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="authorizationToken">author token</param>
        /// <param name="requestId">请求id</param>
        /// <param name="authorizationMethod">请求方式</param>
        /// <returns></returns>
        Task<string> GetAsync(string url, string authorizationToken = null, string requestId = null,
            string authorizationMethod = "Bearer");
    }
}