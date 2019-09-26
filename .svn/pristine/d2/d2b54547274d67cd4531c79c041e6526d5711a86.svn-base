using System.Collections.Generic;
using System.Linq;

namespace General.Core.Extension
{
    /// <summary>
    /// EnumableExtension
    /// </summary>
    public static class EnumableExtension
    {
        /// <summary>
        /// StringJoin
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">数据</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string StringJoin<T>(this IEnumerable<T> list, string separator)
        {
            var enumerable = list as T[] ?? list.ToArray();
            if (enumerable.Any()) return string.Join(separator, enumerable.ToList());
            return "";
        }
    }
}