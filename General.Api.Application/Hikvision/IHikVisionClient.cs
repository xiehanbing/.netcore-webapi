using System.Threading.Tasks;

namespace General.Api.Application.Hikvision
{
    /// <summary>
    /// 海康威视 HTTP 请求 客户端
    /// </summary>
    public interface IHikVisionClient
    {
        /// <summary>
        /// 获取 post 请求  泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">路径</param>
        /// <param name="body">参数</param>
        /// <returns></returns>
        Task<HikVisionResponse<T>> PostAsync<T>(string uri, object body) where T:class;
    }
}