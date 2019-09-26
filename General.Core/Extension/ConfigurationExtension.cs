using System;
using Microsoft.Extensions.Configuration;

namespace General.Core.Extension
{
    /// <summary>
    /// ConfigurationExtension
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// GetAppSettingByKey
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetAppSettingByKey(this IConfiguration configuration, string key)
        {
            return configuration[key];
        }
        /// <summary>
        /// GetAppSetting
        /// Shorthand for GetSection("AppSettings")[key]
        /// </summary>
        /// <param name="configuration">IConfiguration instance</param>
        /// <param name="key">appSettings key</param>
        /// <returns>app setting value</returns>
        public static string GetAppSetting(this IConfiguration configuration, string key)
        {
            return configuration.GetSection("AppSettings")[key];
        }

        /// <summary>
        /// GetAppSetting
        /// Shorthand for GetSection("AppSettings")[key]
        /// </summary>
        /// <param name="configuration">IConfiguration instance</param>
        /// <param name="key">appSettings key</param>
        /// <returns>app setting value</returns>
        public static T GetAppSetting<T>(this IConfiguration configuration, string key)
        {
            return configuration.GetSection("AppSettings")[key].GetDeserializeObject<T>();
        }

        /// <summary>
        /// GetAppSetting
        /// Shorthand for GetSection("AppSettings")[key]
        /// </summary>
        /// <param name="configuration">IConfiguration instance</param>
        /// <param name="key">appSettings key</param>
        /// <param name="defaultValue">default value if not exist</param>
        /// <returns>app setting value</returns>
        public static T GetAppSetting<T>(this IConfiguration configuration, string key, T defaultValue)
        {
            var data = configuration.GetSection("AppSettings")[key].GetDeserializeObject<T>();
            if(data==null)data= defaultValue;
            return data;
        }
    }
}