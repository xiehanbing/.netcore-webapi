using System;
using System.Globalization;

namespace General.Core.Extension
{
    /// <summary>
    /// DateTimeExtension 日期扩展
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="formatter">格式化字符串</param>
        /// <returns></returns>
        public static string TimeToString(this DateTime time, string formatter = "yyyy-MM-dd HH:mm:ss")
        {
            return time.ToString(formatter);
        }
        /// <summary>
        /// 开始查询时间（IOS8601格式：yyyy-MM-ddTHH:mm:ss.SSSXXX）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeIosFormatter(this DateTime time)
        {
            //var sss = time.ToString("yyyy-MM-ddTHH:mm:ss.fff+8:00",DateTimeFormatInfo.InvariantInfo);
            return time.ToString("yyyy-MM-ddTHH:mm:ss.fff+08:00");
        }

        /// <summary>
        /// 开始查询时间（IOS8601格式：yyyy-MM-ddTHH:mm:ss.SSSXXX）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeIosFormatter(this DateTime? time)
        {
            //var sss = time.ToString("yyyy-MM-ddTHH:mm:ss.fff+8:00",DateTimeFormatInfo.InvariantInfo);
            return time?.ToString("yyyy-MM-ddTHH:mm:ss.fff+08:00");
        }
    }
}