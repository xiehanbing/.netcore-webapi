using System;

namespace General.Api.Application.Video.Dto
{
    /// <summary>
    /// 获取监控点回放取流URL 响应
    /// </summary>
    public class PlayackUrlResponse
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 录像片段大小
        /// </summary>
        public int  Size { get; set; }
        /// <summary>
        /// 标记本次查询的标识符，用于查询分片时的多次查询，查询下一片时的入参
        /// </summary>
        public string  Uuid { get; set; }
        /// <summary>
        /// 取流短url
        /// </summary>
        public string  Url { get; set; }
    }
}