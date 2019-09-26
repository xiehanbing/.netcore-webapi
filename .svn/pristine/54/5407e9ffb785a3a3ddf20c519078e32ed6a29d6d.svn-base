using Newtonsoft.Json;

namespace General.Core.Extension
{
    /// <summary>
    /// jsonconvert 扩展类 
    /// </summary>
    public static class JsonConvertExtension
    {
        /// <summary>
        /// 获取序列化字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSerializeObject(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        /// <summary>
        /// 获取反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetDeserializeObject(this string value)
        {
            return JsonConvert.DeserializeObject(value);
        }
        /// <summary>
        /// 获取反序列化 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetDeserializeObject<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}