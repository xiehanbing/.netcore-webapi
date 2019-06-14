using System.Collections.Generic;

namespace General.Api.Application.Visitor.Dto
{
    /// <summary>
    /// 访客
    /// </summary>
    public class VisitorAddResponse
    {
        /// <summary>
        /// 预约记录ID
        /// </summary>
        public string AppointRecordId { get; set; }
        /// <summary>
        /// 预约信息
        /// </summary>

        public List<AppointmentInfo> AppointmentInfoList { get; set; }
    }
    /// <summary>
    /// 预约信息
    /// </summary>
    public class AppointmentInfo
    {
        /// <summary>
        /// 访客姓名
        /// </summary>
        public string VisitorName { get; set; }
        /// <summary>
        /// 被访人唯一标识
        /// </summary>
        public string ReceptionistId { get; set; }
        /// <summary>
        /// 被访人姓名
        /// </summary>
        public string ReceptionistName { get; set; }
        /// <summary>
        /// 访客验证码
        /// </summary>
        public string VerificationCode { get; set; }
        /// <summary>
        /// 访客二维码内容 如需要使用，可将访客二维码内容转成二维码图片即可，可通过第三方工具类将二维码内容转化为二维码图片，长度10位
        /// </summary>
        public string QrCode { get; set; }
    }
}