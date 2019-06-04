using System;
using System.Collections.Generic;
using System.Linq;

namespace General.Core
{
    /// <summary>
    /// 验证异常类
    /// </summary>
    public class ValidatorException : Exception
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public int Code
        { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> Errors { get; set; }
        /// <summary>
        /// construct
        /// </summary>
        public ValidatorException(string message) : base(message)
        {
            Code = 10009;
            var array = message?.Split(";");
            Errors = array?.ToList();
        }
        ///// <summary>
        ///// construct
        ///// </summary>
        //public ValidatorException(int code, string message) : base(message)
        //{
        //    Code = code;
        //}
    }
}