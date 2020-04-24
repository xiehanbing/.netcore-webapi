using System.Threading.Tasks;
using General.Core;
using HttpUtil;

namespace General.Api.Application.Hikvision
{
    /// <summary>
    /// 海康威视 HTTP 请求 客户端
    /// </summary>
    public class HikVisionClient : IHikVisionClient
    {
        private readonly IHikHttpUtillib _hikHttp;
        /// <summary>
        /// construct
        /// </summary>
        /// <param name="hikHttp"></param>
        public HikVisionClient(IHikHttpUtillib hikHttp)
        {
            _hikHttp = hikHttp;
        }
        /// <summary>
        /// <see cref="IHikVisionClient.PostAsync{T}(string,object)"/>
        /// </summary>
        public async Task<HikVisionResponse<T>> PostAsync<T>(string uri, object body) where T : class
        {
            var response = await _hikHttp.PostAsync<HikVisionResponse<T>>(uri, body);
            if (!response.Success)
            {
                throw new MyException($"{response.Msg}");
            }

            return response;
        }
    }
}