using System.ComponentModel;

namespace General.EntityFrameworkCore.Log
{
    /// <summary>
    /// 日志类型枚举（可扩展）
    /// </summary>
    public enum LogTypeEnum
    {
        /// <summary>
        /// 表的日志
        /// </summary>
        [Description("表日志")]
        Table = 1,
        /// <summary>
        /// api请求的日志
        /// </summary>
        [Description("api请求")]
        ApiRequest = 2,
        /// <summary>
        /// 其他日志
        /// </summary>
        [Description("其他日志")]
        Other = 3,
        /// <summary>
        /// 异常日志
        /// </summary>
        [Description("异常日志")]
        Exception =4,
        /// <summary>
        /// Client日志
        /// </summary>
        [Description("Client日志")]
        HttpClient =5,
        /// <summary>
        /// Resource请求日志
        /// </summary>
        [Description("Resource请求日志")]
        Resource =6

    }
}