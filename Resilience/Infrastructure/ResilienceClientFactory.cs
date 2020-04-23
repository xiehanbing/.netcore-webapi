using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;

namespace Resilience.Infrastructure
{
    /// <summary>
    /// ResilienceClientFactory
    /// </summary>
    public class ResilienceClientFactory
    {
        private readonly ILogger<ResilientHttpClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 重试次数
        /// </summary>
        private readonly int _retryCount;
        /// <summary>
        /// 熔断之前允许的异常次数
        /// </summary>
        private readonly int _exceptionCountAllowedBeforeBreaking;

        private readonly string _traceName;
        public ResilienceClientFactory(ILogger<ResilientHttpClient> logger, IHttpContextAccessor httpContextAccessor,
             int retryCount, int exceptionCountAllowedBeforeBreaking, string traceName)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _retryCount = retryCount;
            _exceptionCountAllowedBeforeBreaking = exceptionCountAllowedBeforeBreaking;
            _traceName = traceName;
        }
        /// <summary>
        /// GetResilientHttpClient
        /// </summary>
        /// <returns></returns>
        public ResilientHttpClient GetResilientHttpClient() => new ResilientHttpClient(orign => CreatePolicy(orign), _logger, _httpContextAccessor, _traceName);
        /// <summary>
        /// CreatePolicy
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private Policy[] CreatePolicy(string origin)
        {
            return new Policy[]
            {
                Policy.Handle<HttpRequestException>()
                    .WaitAndRetryAsync(_retryCount,
                        retryAttempt=>TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)),
                        (exception, timeSpan, retryCount, context) =>
                        {
                            var msg =
                                $"第{retryCount}次重试 of {context.PolicyKey} at {context.ExecutionKey}  due to :{exception}.";
                            _logger.LogError(msg);
                        }),
                Policy.Handle<HttpRequestException>()
                    .CircuitBreakerAsync(_exceptionCountAllowedBeforeBreaking,
                        TimeSpan.FromMinutes(1),
                        onBreak:(exception, duration) =>
                        {
                            _logger.LogError("熔断器打开");
                        },
                        onReset:() =>
                        {
                            _logger.LogError("熔断器关闭");
                        })
            };
        }
    }
}