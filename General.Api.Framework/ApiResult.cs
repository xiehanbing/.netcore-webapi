namespace General.Api.Framework
{
    /// <summary>
    /// 自定义返回接口类型
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 响应码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 默认构造
        /// </summary>
        public ApiResult()
        {
            Success = false;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        public ApiResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">消息</param>
        /// <param name="code">响应码</param>
        public ApiResult(bool success, string message, int code)
        {
            Success = success;
            Message = message;
            Code = code;
        }
    }
    /// <summary>
    /// 自定义返回接口类型 （结果）
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ApiResult<T> : ApiResult where T : class
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 默认构造
        /// </summary>
        public ApiResult()
        {
            Success = false;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        public ApiResult(T data)
        {
            Success = true;
            Result = data;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        /// <param name="success"></param>
        public ApiResult(bool success)
        {
            Success = success;
        }
        /// <summary>
        /// 成功加数据
        /// </summary>
        /// <param name="success"></param>
        /// <param name="data"></param>
        public ApiResult(bool success, T data)
        {
            Success = success;
            Result = data;
        }
    }
}