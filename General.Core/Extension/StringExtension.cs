namespace General.Core.Extension
{
    /// <summary>
    /// string 扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 字符串拼接
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="input">要拼接的</param>
        /// <returns></returns>
        public static string StringCombine(this string source, string input)
        {
            return source + input;
        }
        /// <summary>
        /// 判断是否为null或者为空
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsNull(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
        /// <summary>
        /// 判断是否不为空和不为null
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsNotNull(this string input)
        {
            return !string.IsNullOrEmpty(input);
        }
        /// <summary>
        /// 是否为空字符串或null
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
        /// <summary>
        /// 是否不为空字符串和null
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsNotWhiteSpace(this string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}