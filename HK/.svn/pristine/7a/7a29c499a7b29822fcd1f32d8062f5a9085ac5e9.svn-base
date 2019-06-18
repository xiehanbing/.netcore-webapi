using System;

namespace General.Api.Application.EventRevice.Dto.VerifyPerson
{
    /// <summary>
    /// 门禁认证对比成功接收实体
    /// </summary>
    public class VerifyPersonDto
    {
        /// <summary>
        /// 人员通道号
        /// </summary>
        public int? ExtAccessChannel { get; set; }
        /// <summary>
        /// 报警输入/防区通道
        /// </summary>
        public int?  ExtEventAlarmId { get; set; }
        /// <summary>
        /// 报警输出通道
        /// </summary>
        public int? ExtEventAlarmOutId { get; set; }
        /// <summary>
        /// 卡号（身份证号）
        /// </summary>
        public string ExtEventCardNo { get; set; }
        /// <summary>
        /// 事件输入通道
        /// </summary>
        public int? ExtEventCaseId { get; set; }
        /// <summary>
        /// 事件类型代码
        /// </summary>
        public int? ExtEventCode { get; set; }
        /// <summary>
        /// 通道事件信息
        /// </summary>
        public ExtEventCustomerNumInfoDto ExtEventCustomerNumInfo { get; set; }
        /// <summary>
        /// 门编号
        /// </summary>
        public int?  ExtEventDoorId { get; set; }
        /// <summary>
        /// 身份证照片
        /// </summary>
        public string  ExtEventIdCardPictureUrl { get; set; }
        /// <summary>
        /// 进出方向 1-进、0-出
        /// </summary>
        public int? ExtEventInOut { get; set; }
        /// <summary>
        /// 就地控制器id
        /// </summary>
        public int? ExtEventLocalControllerId { get; set; }
        /// <summary>
        /// 主设备拨码
        /// </summary>
        public int?  ExtEventMainDevId { get; set; }
        /// <summary>
        /// 图片的url
        /// </summary>
        public string  ExtEventPictureUrl { get; set; }
        /// <summary>
        /// 图片服务器唯一编码
        /// </summary>
        public string  SvrIndexCode { get; set; }
        /// <summary>
        /// 读卡器id
        /// </summary>
        public int?  ExtEventReaderId { get; set; }
        /// <summary>
        /// 读卡器类别  	0-无效，1-IC读卡器，2-身份证读卡器，3-二维码读卡器,4-指纹头
        /// </summary>
        public int? ExtEventReaderKind { get; set; }
        /// <summary>
        /// 报告上传通道 1-布防上传，2-中心组1上传，3-中心组2上传，为0无效
        /// </summary>
        public int? ExtEventReportChannel { get; set; }
        /// <summary>
        /// 群组编号
        /// </summary>
        public int? ExtEventRoleId { get; set; }
        /// <summary>
        /// 分控制器硬件ID
        /// </summary>
        public int? ExtEventSubDevId { get; set; }
        /// <summary>
        /// 刷卡次数
        /// </summary>
        public int? ExtEventSwipNum { get; set; }
        /// <summary>
        /// 事件类型  事件类型，如普通门禁事件为0,身份证信息事件为1，客流量统计为2
        /// </summary>
        public int? ExtEventType { get; set; }
        /// <summary>
        /// 多重认证序号
        /// </summary>
        public int? ExtEventVerifyId { get; set; }
        /// <summary>
        /// 事件上报驱动的时间
        /// </summary>
        public string  ExtReceiveTime { get; set; }
        /// <summary>
        /// 事件流水号 事件流水号，为0无效
        /// </summary>
        public int  Seq { get; set; }
        /// <summary>
        /// 白名单单号  	1-8，为0无效
        /// </summary>
        public int?  ExtEventWhiteListNo { get; set; }
        /// <summary>
        /// 人员身份证信息 在设备上刷身份证进行认证时，该属性才会有对应人员的身份证信息
        /// </summary>
        public ExtEventIdentityCardInfoDto ExtEventIdentityCardInfo { get; set; }
    }
    /// <summary>
    /// 通道事件信息
    /// </summary>
    public class ExtEventCustomerNumInfoDto
    {
        /// <summary>
        /// 通道号
        /// </summary>
        public int? AccessChannel { get; set; }
        /// <summary>
        /// 进人数
        /// </summary>
        public int? EntryTimes { get; set; }
        /// <summary>
        /// 出人数
        /// </summary>
        public int? ExitTimes { get; set; }
        /// <summary>
        /// 总通行人数
        /// </summary>
        public int? TotalTimes { get; set; }
    }

    /// <summary>
    /// 人员身份证信息 在设备上刷身份证进行认证时，该属性才会有对应人员的身份证信息
    /// </summary>
    public class ExtEventIdentityCardInfoDto
    {
        /// <summary>
        /// 住址
        /// </summary>
        public string  Address { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birth { get; set; }
        /// <summary>
        /// 有效日期结束时间
        /// </summary>
        public DateTime  EndDate { get; set; }
        /// <summary>
        /// 身份证id
        /// </summary>
        public string  IdNum { get; set; }
        /// <summary>
        /// 签发机关
        /// </summary>
        public string  IssuingAuthority { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string  Name { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public int Nation { get; set; }
        /// <summary>
        /// 性别，1-男，2-女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 有效日期开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 是否长期有效  0-否（有效截止日期有效），1-是（有效截止日期无效）
        /// </summary>
        public int TermOfValidity { get; set; }
    }
}