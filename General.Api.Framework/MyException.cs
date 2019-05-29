using System;

namespace General.Api.Framework
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class MyException : Exception
    {
        public int Code
        { get; set; }
        public MyException(string message) : base(message)
        { }

        public MyException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}