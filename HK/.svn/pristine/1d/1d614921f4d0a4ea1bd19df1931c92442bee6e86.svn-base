using System;
using System.ComponentModel;
using System.Threading.Tasks;
using General.Core.Extension;

namespace General.Api.Application.Spcms.Dto
{
    /// <summary>
    /// 报警事件日志
    /// </summary>
    public class EventLogResponse
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int EventType { get; set; }
        /// <summary>
        /// 事件源indexCode
        /// </summary>
        public string SrcIndex { get; set; }
        /// <summary>
        /// 事件源名称
        /// </summary>
        public string SrcName { get; set; }
        /// <summary>
        /// 事件源资源类型
        /// </summary>
        public string SrcType { get; set; }
        /// <summary>
        /// 事件发生区域
        /// </summary>
        public string RegionId { get; set; }
        /// <summary>
        /// 事件状态
        /// </summary>
        public EventStatusType Status
        { get; set; }
        /// <summary>
        /// 事件状态描述
        /// </summary>
        public string StatusDesc => typeof(EventStatusType).GetEnumDescription(Status.GetHashCode());
    }
    /// <summary>
    /// 事件状态
    /// </summary>
    public enum EventStatusType
    {
        /// <summary>
        /// 瞬时
        /// </summary>
        [Description("瞬时")]
        ShunShi = 0,
        /// <summary>
        /// 开始
        /// </summary>
        [Description("开始")]
        Start = 1,
        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        Stop = 2,
        /// <summary>
        /// 事件脉冲
        /// </summary>
        [Description("事件脉冲")]
        MaiChong = 3,
        /// <summary>
        /// 事件联动结果更新
        /// </summary>
        [Description("事件联动结果更新")]
        LianDong = 4
    }
}