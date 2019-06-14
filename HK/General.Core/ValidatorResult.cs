using System.Collections.Generic;

namespace General.Core
{
    /// <summary>
    /// 参数校验结果
    /// </summary>
    public class ValidatorResult
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string  Message { get; set; }
        /// <summary>
        /// 响应码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误
        /// </summary>
        public List<string> Errors { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
    }
}