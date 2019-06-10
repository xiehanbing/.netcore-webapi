using System;
using System.Collections.Generic;
using General.Api.Application.User.Dto;

namespace General.Api.Application.Visitor.Request
{
    /// <summary>
    /// 访客预约类
    /// </summary>
    public class VisitorAddRequest
    {
        /// <summary>
        /// 预约记录ID 只针对修改
        /// </summary>
        public string AppointRecordId { get; set; }
        /// <summary>
        /// 被访人唯一标识
        /// </summary>
        public string ReceptionistId { get; set; }
        /// <summary>
        /// 预计来访时间
        /// </summary>
        public DateTime VisitStartTime { get; set; }
        /// <summary>
        /// 预计离开时间
        /// </summary>
        public DateTime VisitEndTime { get; set; }
        /// <summary>
        /// 来访事由
        /// </summary>
        public string VisitPurpose { get; set; }
        /// <summary>
        /// 访客信息列表
        /// </summary>
        public List<VisitorInfo> VisitorInfoList { get; set; }
    }
    /// <summary>
    /// 访客信息
    /// </summary>
    public class VisitorInfo
    {
        /// <summary>
        /// 访客姓名
        /// </summary>
        public string VisitorName { get; set; }
        /// <summary>
        /// 访客性别
        /// </summary>
        public GenderType Gender { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNo { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 证件类型 111：身份证，只支持身份证
        /// </summary>
        public int? CertificateType { get; set; }
        /// <summary>
        /// 身份证号 1~20个数字、字母组成； 证件号码非空时，证件类型必填；
        /// </summary>
        public string CertificateNo { get; set; }
    }
}