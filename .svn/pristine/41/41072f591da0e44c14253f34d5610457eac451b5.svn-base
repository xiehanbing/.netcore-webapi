using System;
using System.ComponentModel;
using System.IO;

namespace General.Api.Application.Visitor.Dto
{
    /// <summary>
    /// 访客信息记录
    /// </summary>
    public class AppointmentRecordResponse
    {
        /// <summary>
        /// 预约记录ID
        /// </summary>
        public string AppointRecordId { get; set; }
        /// <summary>
        /// 被访人唯一标识
        /// </summary>
        public string ReceptionistId { get; set; }
        /// <summary>
        /// 被访人姓名
        /// </summary>
        public string ReceptionistName { get; set; }
        /// <summary>
        /// 被访人所属组织编码
        /// </summary>
        public string ReceptionistCode { get; set; }
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
        /// 访客姓名
        /// </summary>
        public string VisitorName { get; set; }
        /// <summary>
        /// 访客验证码
        /// </summary>
        public string VerificationCode { get; set; }
        /// <summary>
        /// 访客二维码内容
        /// </summary>
        public string QrCode { get; set; }
        /// <summary>
        /// 性别 1-男, 2-女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNo { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 证件类型 111：身份证
        /// </summary>
        public int? CertificateType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertificateNo { get; set; }
        /// <summary>
        /// 访客状态
        /// </summary>
        public VisitorStatusType VisitorStatus { get; set; }
        /// <summary>
        /// 访客头像
        /// </summary>
        public string PicUri { get; set; }
        /// <summary>
        /// 图片存储服务的唯一标识
        /// </summary>
        public string SvrIndexCode { get; set; }

    }
    /// <summary>
    /// 访客状态
    /// </summary>
    public enum VisitorStatusType
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 迟到
        /// </summary>
        [Description("迟到")]
        After = 2,
        /// <summary>
        /// 失效
        /// </summary>
        [Description("失效")]
        UnActive = 3,
        /// <summary>
        /// 超期自动签离
        /// </summary>
        [Description("超期自动签离")]
        AutoQianLi=5,
        /// <summary>
        /// 已签离
        /// </summary>
        [Description("已签离")]
        QianLi=6,
        /// <summary>
        /// 超期未签离
        /// </summary>
        [Description("超期未签离")]
        AutoUnQianLi=7,
        /// <summary>
        /// 已到达
        /// </summary>
        [Description("已到达")]
        Arrive=8
    }
}