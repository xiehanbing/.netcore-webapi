namespace General.HttpClient.Extension
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
    }
}