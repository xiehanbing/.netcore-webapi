using System.Net;
using System.Threading.Tasks;

namespace HttpUtil
{
    public interface IHikHttpUtillib
    {
        /// <summary>
        /// 获取 get 请求 byte[] 类型
        /// </summary>
        /// <param name="uri">路径</param>
        /// <returns></returns>
        Task<byte[]> GetByteAsync(string uri);
        /// <summary>
        /// 获取 post 请求 byte[] 类型
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="body">参数</param>
        /// <returns></returns>
        Task<byte[]> PostByteAsync(string uri, string body);
        /// <summary>
        /// 获取 post 请求 string 类型
        /// </summary>
        /// <param name="uri">路径</param>
        /// <returns></returns>
        Task<string> GetStringAsync(string uri);
        /// <summary>
        /// 获取 post 请求 string 类型
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="body">参数</param>
        /// <returns></returns>
        Task<string> PostStringAsync(string uri, object body);
        /// <summary>
        /// 获取get 请求  泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">路径</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string uri);
        /// <summary>
        /// 获取 post 请求   发型
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="body">参数</param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string uri, object body);
        /// <summary>
        /// 获取 post 请求   HttpWebResponse
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<HttpWebResponse> PostHttpWebResponseAsync(string uri, object body);

    }
}