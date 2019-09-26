using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace General.Core.HttpClient.Extension
{
    /// <summary>
    /// response 返回结果
    /// </summary>
    public static class ResponseExtension
    {
        #region get result

        /// <summary>
        /// 获取repsonse结果
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="response">response</param>
        /// <returns></returns>
        public static T GetJsonResult<T>(this HttpContent response)
        {
            var data = response.ReadAsStringAsync().Result;
            response.Dispose();
            return JsonConvert.DeserializeObject<T>(data);
        }
        /// <summary>
        /// 获取repsonse结果
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="response">response</param>
        /// <returns></returns>
        public static async Task<T> GetJsonResultAsync<T>(this Task<HttpContent> response)
        {
            var resp = await response;
            var data = await resp.ReadAsStringAsync();
            response.Dispose();
            return JsonConvert.DeserializeObject<T>(data);
        }
        /// <summary>
        /// 获取默认的response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetJsonResult(this HttpContent response)
        {
            return response.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// 获取默认的response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<string> GetJsonResultAsync(this Task<HttpContent> response)
        {
            var resp = await response;
            return await resp.ReadAsStringAsync();
        }

        #endregion



        #region get result

        /// <summary>
        /// 获取repsonse结果
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="response">response</param>
        /// <returns></returns>
        public static T ReciveJsonResult<T>(this HttpResponseMessage response)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            response.Dispose();
            return JsonConvert.DeserializeObject<T>(data);
        }
        /// <summary>
        /// 获取repsonse结果
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="response">response</param>
        /// <returns></returns>
        public static async Task<T> ReciveJsonResultAsync<T>(this Task<HttpResponseMessage> response)
        {
            var resp = await response;
            var data = await resp.Content.ReadAsStringAsync();
            response.Dispose();
            return JsonConvert.DeserializeObject<T>(data);
        }
        /// <summary>
        /// 获取相应头
        /// </summary>
        /// <param name="response">HttpResponseMessage </param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static async Task<List<string>> ReciveResponseHeadersByKey(this Task<HttpResponseMessage> response, string key)
        {
            var resp = await response;
            return resp.Headers.FirstOrDefault(o => o.Key.Equals(key)).Value?.ToList();
        }
        /// <summary>
        /// 获取所有响应头
        /// </summary>
        /// <param name="response">HttpResponseMessage</param>
        /// <returns></returns>
        public static async Task<HttpResponseHeaders> ReciveResponseHeaders(this Task<HttpResponseMessage> response)
        {
            var resp = await response;
            return resp.Headers;
        }
        /// <summary>
        /// 获取相应头
        /// </summary>
        /// <param name="response">HttpContent </param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static async Task<List<string>> GetResponseHeaders(this Task<HttpContent> response, string key)
        {
            var resp = await response;
            return resp.Headers.FirstOrDefault(o => o.Key.Equals(key)).Value?.ToList();
        }
        /// <summary>
        /// 获取所有响应头
        /// </summary>
        /// <param name="response">HttpContent</param>
        /// <returns></returns>
        public static async Task<HttpContentHeaders> GetResponseHeaders(this Task<HttpContent> response)
        {
            var resp = await response;
            return resp.Headers;
        }
        /// <summary>
        /// 获取默认的response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ReciveJsonResult(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// 获取默认的response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<string> ReciveJsonResultAsync(this Task<HttpResponseMessage> response)
        {
            var resp = await response;
            return await resp.Content.ReadAsStringAsync();
        }

        #endregion
    }
}