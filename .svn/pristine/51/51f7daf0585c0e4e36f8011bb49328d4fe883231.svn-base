using System;
using System.ComponentModel;
using General.Core.Extension;

namespace General.Api.Application.Door.Dto
{
    /// <summary>
    /// 门禁事件响应结果
    /// </summary>
    public class DoorEventQueryResponse
    {
        /// <summary>
        /// 事件唯一编号
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int  EventType { get; set; }
        /// <summary>
        /// 事件名称
        /// </summary>
        public string  EventName { get; set; }
        /// <summary>
        /// 事件产生时间
        /// </summary>
        public DateTime EventTime { get; set; }
        /// <summary>
        /// 事件产生时间
        /// </summary>
        public string  PersonId { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string  PersonName { get; set; }
        /// <summary>
        /// 组织编码
        /// </summary>
        public string  OrgIndexCode { get; set; }
        /// <summary>
        /// 门禁点名称
        /// </summary>
        public string  DoorName { get; set; }
        /// <summary>
        /// 门禁点编码
        /// </summary>
        public string  DoorIndexCode { get; set; }
        /// <summary>
        /// 门禁点所在区域编码
        /// </summary>
        public string  DoorRegionIndexCode { get; set; }
        /// <summary>
        /// 进出类型
        /// </summary>
        public InAndOutType InAndOutType { get; set; }
        /// <summary>
        /// 进出类型描述
        /// </summary>
        public string InAndOutTypeDesc => typeof(InAndOutType).GetEnumDescription(InAndOutType.GetHashCode());
        /// <summary>
        /// 抓拍图片地址
        /// </summary>
        public string  PicUri { get; set; }
        /// <summary>
        /// 图片存储服务的唯一标识
        /// </summary>
        public string  SvrIndexCode { get; set; }
    }
    /// <summary>
    /// 进出类型
    /// </summary>
    public enum InAndOutType
    {
        /// <summary>
        /// 进
        /// </summary>
        [Description("进")]
        In=1,
        /// <summary>
        /// 出
        /// </summary>
        [Description("出")]
        Out=0,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnow=-1
    }
    /// <summary>
    /// 
    /// </summary>
    public enum DoorEventCompairType
    {

    }
}