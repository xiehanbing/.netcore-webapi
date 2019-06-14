using System;

namespace General.Log.Entity
{
    /// <summary>
    /// 日志实体
    /// </summary>
    [Serializable]
    public class LogEntity
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime LogDate { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string Ip { get; set; }
    }
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        Info = 1,
        Debug = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5
    }
}