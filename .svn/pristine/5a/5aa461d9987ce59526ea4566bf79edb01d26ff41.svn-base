using System;
using General.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace General.Api.Engine
{
    /// <summary>
    /// 引擎实现 (不从构造函数中也可获取实例)
    /// </summary>
    public class GeneralEngine : IEngine
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 构造
        /// </summary>
        public GeneralEngine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 构建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }
        /// <summary>
        /// <see cref="IEngine.ResolveByRequireService{T}()"/>
        /// </summary>
        public T ResolveByRequireService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}