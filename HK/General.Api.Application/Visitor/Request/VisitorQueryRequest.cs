using System;
using General.Api.Application.Visitor.Dto;

namespace General.Api.Application.Visitor.Request
{
    /// <summary>
    /// 查询访客预约记录
    /// </summary>
    public class VisitorQueryRequest : PageRequest
    {
        /// <summary>
        /// 被访人唯一标识
        /// </summary>
        public string ReceptionistId { get; set; }
        /// <summary>
        /// 访客姓名
        /// </summary>
        public string VisitorName { get; set; }
        /// <summary>
        /// 访客联系电话
        /// </summary>
        public string PhoneNo { get; set; }
        /// <summary>
        /// 访客验证码
        /// </summary>
        public string VerificationCode { get; set; }
        /// <summary>
        /// 访客状态
        /// </summary>
        public VisitorStatusType? VisitorStatus { get; set; }
        /// <summary>
        /// 预计来访时间查询时间段条件的开始时间
        /// </summary>
        public DateTime? VisitStartTimeBegin { get; set; }
        /// <summary>
        /// 预计来访时间查询时间段条件的结束时间
        /// </summary>
        public DateTime? VisitStartTimeEnd { get; set; }
        /// <summary>
        /// 预计离开时间查询时间段条件的开始时间
        /// </summary>
        public DateTime? VisitEndTimeBegin { get; set; }
        /// <summary>
        /// 预计离开时间查询时间段条件的结束时间
        /// </summary>
        public DateTime? VisitEndTimeEnd { get; set; }
    }
}