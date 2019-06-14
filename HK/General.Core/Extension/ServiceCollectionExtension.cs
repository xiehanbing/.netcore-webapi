using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using General.Core.Libs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using General.Core.Data;

namespace General.Core.Extension
{
    /// <summary>
    /// IServiceCollection 容器的扩展类
    /// </summary>
    public static class ServiceCollextionExtension
    {
        /// <summary>
        /// 程序集注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="serviceLifetime">生命周期</param>
        public static void AddAssembly(this IServiceCollection services, string assemblyName, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services) + "为空");

            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName) + "为空");

            //get services  assembly 获取指定程序集 
            var serviceAssembly = RuntimeHelper.GetAssemblyByName(assemblyName);
            if (serviceAssembly == null)
            {
                throw new ArgumentNullException(nameof(serviceAssembly) + ".dll 不存在");
            }
            var types = serviceAssembly.GetTypes();

            //去除抽象类和泛型类
            var list = types.Where(o => o.IsClass && !o.IsAbstract && !o.IsGenericType).ToList();
            foreach (var type in list)
            {
                var interfaceList = type.GetInterfaces();

                if (interfaceList.Any())
                {
                    var typeInterfac = interfaceList.First();
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(typeInterfac, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(typeInterfac, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(typeInterfac, type);
                            break;
                        default:
                            services.AddScoped(typeInterfac, type);
                            break;
                    }
                }
            }

        }
    }
}