using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Wrap;
using zipkin4net.Transport.Http;

namespace Resilience
{
    public class ResilientHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;
        //根据 url origin  创建 policy
        private readonly Func<string, IEnumerable<Polly.Policy>> _poliyCreator;
        //把 policy 打包成组合 policy-wraper 进行本地缓存
        private readonly ConcurrentDictionary<string, PolicyWrap> _policyWraps;
        private readonly ILogger<ResilientHttpClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly string _traceName;
        public ResilientHttpClient(Func<string, IEnumerable<Polly.Policy>> poliyCreator, ILogger<ResilientHttpClient> logger, IHttpContextAccessor httpContextAccessor,string traceName="")
        {
            _httpClient = new HttpClient(new TracingHandler(traceName));
            _poliyCreator = poliyCreator;
            _policyWraps = new ConcurrentDictionary<string, PolicyWrap>();
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
           // _traceName = traceName;
        }
        /// <summary>
        /// <see cref="IHttpClient.PostAsync{T}(string,T,string,string,string)"/>
        /// </summary>
        public async Task<T> PostAsync<T>(string url, T item, string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            HttpRequestMessage RequestMessage() => CreateHttpRequestMessage(HttpMethod.Post, url, item);
            var response =await DoPostAsync(HttpMethod.Post, url, RequestMessage, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            return default(T);
        }
        /// <summary>
        /// <see cref="IHttpClient.PostStringAsync{T}(string,T,string,string,string)"/>
        /// </summary>
        public async Task<string> PostStringAsync<T>(string url, T item, string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            HttpRequestMessage RequestMessage() => CreateHttpRequestMessage(HttpMethod.Post, url, item);
            var response = await DoPostAsync(HttpMethod.Post, url, RequestMessage, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            return string.Empty;
        }
        /// <summary>
        /// <see cref="IHttpClient.PostStringAsync{T}(string,T,string,string,string)"/>
        /// </summary>
        public async Task<T> PostAsync<T>(string url, object item, string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            HttpRequestMessage RequestMessage() => CreateHttpRequestMessage(HttpMethod.Post, url, item);
            var response = await DoPostAsync(HttpMethod.Post, url, RequestMessage, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            return default(T);
        }
        /// <summary>
        /// <see cref="IHttpClient.PostAsync(string,object,string,string,string)"/>
        /// </summary>
        public async Task<string> PostAsync(string url, object item, string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            HttpRequestMessage RequestMessage() => CreateHttpRequestMessage(HttpMethod.Post, url, item);
            var response = await DoPostAsync(HttpMethod.Post, url, RequestMessage, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            return string.Empty;
        }

        /// <summary>
        /// <see cref="IHttpClient.PostAsync{T}(string,Dictionary{string,string},string,string,string)"/>
        /// </summary>
        public async Task<T> PostAsync<T>(string url, Dictionary<string, string> form, string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            HttpRequestMessage RequestMessage() => CreateHttpRequestMessage(HttpMethod.Post, url, form);
            var response =await DoPostAsync(HttpMethod.Post, url, RequestMessage, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                _logger.LogError($"http request url:{url}, response is error , the message is : status code is {response.StatusCode}");
            }
            return default(T);
        }
        /// <summary>
        /// <see cref="IHttpClient.PostAsync(string,Dictionary{string,string},string,string,string)"/>
        /// </summary>
        public async Task<string> PostAsync(string url, Dictionary<string, string> form, string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            HttpRequestMessage RequestMessage() => CreateHttpRequestMessage(HttpMethod.Post, url, form);
            var response = await DoPostAsync(HttpMethod.Post, url, RequestMessage, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            return string.Empty;
        }
        /// <summary>
        /// <see cref="IHttpClient.GetAsync{T}(string,string,string,string)"/>
        /// </summary>
        public async  Task<T> GetAsync<T>(string url, string authorizationToken = null, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var response = await DoGetAsync(HttpMethod.Get, url, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            return default(T);
        }
        /// <summary>
        /// <see cref="IHttpClient.GetAsync(string,string,string,string)"/>
        /// </summary>
        public async Task<string> GetAsync(string url, string authorizationToken = null, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var response = await DoGetAsync(HttpMethod.Get, url, authorizationToken, requestId, authorizationMethod);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            return string.Empty;
        }
        /// <summary>
        /// do post  请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="requestMessageAction"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        private Task<HttpResponseMessage> DoPostAsync(HttpMethod method, string url, Func<HttpRequestMessage> requestMessageAction,
            string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put", nameof(method));
            }

            var origin = GetOriginFromUri(url);
            return HttpInvokeAync(origin, async () =>
            {
                var requestMessage = requestMessageAction();

                SetAuthorizationHeader(requestMessage);
                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                if (requestId != null)
                {
                    requestMessage.Headers.Add("x-requestid", requestId);
                }

                var response = await _httpClient.SendAsync(requestMessage);
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException(url);
                }
                return response;
            });
        }
        /// <summary>
        /// do get 请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        private Task<HttpResponseMessage> DoGetAsync(HttpMethod method, string url,
            string authorizationToken, string requestId = null,
            string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Get && method != HttpMethod.Delete)
            {
                throw new ArgumentException("Value must be either get or delete", nameof(method));
            }

            var origin = GetOriginFromUri(url);
            return HttpInvokeAync(origin, async () =>
            {
                var requestMessage = new HttpRequestMessage(method, url);

                SetAuthorizationHeader(requestMessage);
                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                if (requestId != null)
                {
                    requestMessage.Headers.Add("x-requestid", requestId);
                }

                var response = await _httpClient.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException(url);
                }
                return response;
            });
        
        }
        /// <summary>
        /// HttpInvokeAync 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origin"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<T> HttpInvokeAync<T>(string origin, Func<Task<T>> action)
        {
            var normalizeOrigin = NormalizeOrigin(origin);
            if (!_policyWraps.TryGetValue(normalizeOrigin, out PolicyWrap policyWrap))
            {
                policyWrap = Policy.WrapAsync(_poliyCreator(normalizeOrigin).ToArray());
                _policyWraps.TryAdd(normalizeOrigin, policyWrap);
            }

            return await policyWrap.ExecuteAsync(action,new Context(origin));
        }


        /// <summary>
        /// 格式化 origin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string NormalizeOrigin(string origin)
        {
            return origin?.Trim().ToLower();
        }
        /// <summary>
        /// 根据uri 获取 origin
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static string GetOriginFromUri(string uri)
        {
            var url = new Uri(uri);
            var origin = $"{url.Scheme}://{url.DnsSafeHost}:{url.Port}";
            return origin;
        }
        /// <summary>
        /// 设置 验证头 信息
        /// </summary>
        /// <param name="requestMessage"></param>
        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }
        /// <summary>
        /// CreateHttpRequestMessage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private HttpRequestMessage CreateHttpRequestMessage<T>(HttpMethod method, string url, T item)
        {
            var requestMessage = new HttpRequestMessage(method, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
            };
            return requestMessage;
        }
        /// <summary>
        /// CreateHttpRequestMessage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        private HttpRequestMessage CreateHttpRequestMessage<T>(HttpMethod method, string url, Dictionary<string, string> form)
        {
            var requestMessage = new HttpRequestMessage(method, url) { Content = new FormUrlEncodedContent(form) };
            return requestMessage;
        }
    }
}