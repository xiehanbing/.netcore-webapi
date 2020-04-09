using System;
using System.Collections.Generic;

namespace General.Api.Application.Video.Dto
{
    /// <summary>
    /// 获取监控点回放取流URL 响应
    /// </summary>
    public class PlayackUrlResponse
    {
        /// <summary>
        /// 取流短url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 分页标记
        /// </summary>
        public string UuId { get; set; }
        /// <summary>
        /// 录像片段信息，按时间从前往后排序
        /// </summary>
        public List<PlayackUrlItems> List { get; set; }
    }
    /// <summary>
    /// 录像片段信息，按时间从前往后排序
    /// </summary>
    public class PlayackUrlItems
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 录像片段大小
        /// </summary>
        public long Size { get; set; }
    }
}