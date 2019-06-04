using System.Net.Http;
using System.Threading.Tasks;
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
            return JsonConvert.DeserializeObject<T>(data);
        }
        /// <summary>
        /// 获取repsonse结果
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="response">response</param>
        /// <returns></returns>
        public static async Task<T> GetJsonResultAsync<T>(this HttpContent response)
        {
            var data = await response.ReadAsStringAsync();
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
        public static async Task<string> GetJsonResultAsync(this HttpContent response)
        {
            return await response.ReadAsStringAsync();
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
            return JsonConvert.DeserializeObject<T>(data);
        }
        /// <summary>
        /// 获取repsonse结果
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="response">response</param>
        /// <returns></returns>
        public static async Task<T> ReciveJsonResultAsync<T>(this HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
        /// <summary>
        /// 获取默认的response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string ReciveJsonResult(this HttpResponseMessage response)
        {
            return  response.Content.ReadAsStringAsync().Result;
        }
        /// <summary>
        /// 获取默认的response
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<string> ReciveJsonResultAsync(this HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        #endregion
    }
}