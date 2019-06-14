namespace General.Api.Application
{
    /// <summary>
    /// 海康威视返回结果
    /// </summary>
    public class HikVisionResponse
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success => (Code?.Trim() ?? "").Equals("0");
        /// <summary>
        /// 返回描述
        /// </summary>
        public string Msg { get; set; }
    }
    /// <summary>
    /// 海康威视返回结果 扩展
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HikVisionResponse<T> : HikVisionResponse where T : class
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}