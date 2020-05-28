using System.ComponentModel;
using General.Core.Libs;

namespace General.Api.Application.Parking.Dto.ChargeBill
{
    /// <summary>
    /// 缴费账单
    /// </summary>
    public class ChargeBillResponse
    {
        /// <summary>
        /// 缴费记录唯一标识
        /// </summary>
        public string RecordSysCode { get; set; }
        /// <summary>
        /// 停车库唯一标识
        /// </summary>
        public string ParkSysCode { get; set; }
        /// <summary>
        /// 停车库名称
        /// </summary>
        public string ParkName { get; set; }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 收费规则唯一标识
        /// </summary>
        public string ChargeRuleSysCode { get; set; }
        /// <summary>
        /// 收费规则名称
        /// </summary>
        public string ChargeRuleName { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public string SupposeCost { get; set; }
        /// <summary>
        /// 减免金额
        /// </summary>
        public string DeduceCost { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public string Cost { get; set; }
        /// <summary>
        /// 账单总金额
        /// </summary>
        public string TotalCost { get; set; }
        /// <summary>
        /// 异常收费规则唯一标识
        /// </summary>
        public string ExceptionRuleSysCode { get; set; }
        /// <summary>
        /// 异常收费规则名称否
        /// </summary>
        public string ExceptionRuleName { get; set; }
        /// <summary>
        /// 优惠规则唯一标识
        /// </summary>
        public string ReductRuleSysCode { get; set; }
        /// <summary>
        /// 优惠规则名称
        /// </summary>
        public string ReductRuleName { get; set; }
        /// <summary>
        /// 优惠类型
        ///0：优惠金额
        ///1：折扣
        ///2：全免
        ///4：优惠时长
        /// </summary>
        public int? ReductType { get; set; }
        /// <summary>
        /// 优惠类型 名称
        /// </summary>
        public string ReductTypeName => EnumHelper.GetEnumDescriptionByValue(typeof(ReductTypeEnum), ReductType ?? -1);
        /// <summary>
        /// 收费来源
        ///1：岗亭客户端
        ///2：人工客户端
        ///3：自助客户端
        ///4：收费宝
        ///5：第三方
        ///6：手机
        /// </summary>
        public int? ChargeSource { get; set; }
        /// <summary>
        /// 收费来源 名称
        /// </summary>
        public string ChargeSourceName => EnumHelper.GetEnumDescriptionByValue(typeof(ChargeSourceEnum), ChargeSource ?? -1);
        /// <summary>
        /// 收费方式
        /// 1：现金
        /// 2：储值账户
        /// 3：第三方
        /// 4：支付宝
        /// 5：微信
        /// 6：无感支付
        /// </summary>
        public int? ChargeType { get; set; }
        /// <summary>
        /// 收费方式 名称
        /// </summary>
        public string ChargeTypeName => EnumHelper.GetEnumDescriptionByValue(typeof(ChargeTypeEnum), ChargeType ?? -1);
        /// <summary>
        /// 0：其他车
        ///1：小型车
        ///2：大型车
        ///3：摩托车
        /// </summary>
        public int? VehicleType { get; set; }
        /// <summary>
        /// 车型 名称
        /// </summary>
        public string VehicleTypeName => EnumHelper.GetEnumDescriptionByValue(typeof(VehicleTypeEnum), VehicleType ?? -1);
        /// <summary>
        /// 车辆入场时间，ISO8601格式：
        ///yyyy-MM-ddTHH:mm:ss+当前时区，例如北京时间：
        ///2018-07-26T15:00:00+08:00
        /// </summary>
        public string InTime { get; set; }
        /// <summary>
        /// 缴费时间，ISO8601格式：
        ///yyyy-MM-ddTHH:mm:ss+当前时区，例如北京时间：
        ///2018-07-26T15:00:00+08:00
        /// </summary>
        public string PayTime { get; set; }
        /// <summary>
        /// 车辆入场记录唯一标识
        /// </summary>
        public string EnRecordSysCode { get; set; }
        /// <summary>
        /// 车辆出场记录唯一标识
        /// </summary>
        public string ExRecordSysCode { get; set; }
        /// <summary>
        /// 本次减免金额,单位：元
        /// </summary>
        public string DiscountAmount { get; set; }
        /// <summary>
        /// 优惠券码
        /// </summary>
        public string CouponCode { get; set; }
        /// <summary>
        /// 停车时长,单位：分钟
        /// </summary>
        public long? ParkTime { get; set; }
        /// <summary>
        /// 出入口唯一标识
        /// </summary>
        public string EntranceSysCode { get; set; }
        /// <summary>
        /// 出入口名称
        /// </summary>
        public string EnTraceName { get; set; }
        /// <summary>
        /// 收费员名称
        /// </summary>
        public string Operator { get; set; }
    }

    /// <summary>
    /// 优惠类型
    /// </summary>
    public enum ReductTypeEnum
    {
        /// <summary>
        /// 优惠金额
        /// </summary>
        [Description("优惠金额")]
        DiscountAmount = 0,
        /// <summary>
        /// 折扣
        /// </summary>
        [Description("折扣")]
        Discount = 1,
        /// <summary>
        /// 全免
        /// </summary>
        [Description("全免")]
        Free = 2,
        /// <summary>
        /// 优惠时长
        /// </summary>
        [Description("优惠时长")]
        DurationOffer = 3
    }
    /// <summary>
    /// 收费来源
    /// </summary>
    public enum ChargeSourceEnum
    {
        /// <summary>
        /// 岗亭客户端
        /// </summary>
        [Description("岗亭客户端")]
        GangWei = 1,
        /// <summary>
        /// 人工客户端
        /// </summary>
        [Description("人工客户端")]
        RenGong = 2,
        /// <summary>
        /// 自助客户端
        /// </summary>
        [Description("自助客户端")]
        ZiZhu = 3,
        /// <summary>
        /// 收费宝
        /// </summary>
        [Description("收费宝")]
        ShouFeiBao = 4,
        /// <summary>
        /// 第三方
        /// </summary>
        [Description("第三方")]
        DiSanFang = 5,
        /// <summary>
        /// 手机
        /// </summary>
        [Description("手机")]
        MobilePhone = 6
    }
    /// <summary>
    /// 收费方式
    /// </summary>
    public enum ChargeTypeEnum
    {
        /// <summary>
        /// 现金
        /// </summary>
        [Description("现金")]
        Cash = 1,
        /// <summary>
        /// 储值账户
        /// </summary>
        [Description("储值账户")]
        ChuZhiZhangHu = 2,
        /// <summary>
        /// 第三方
        /// </summary>
        [Description("第三方")]
        DiSanFang = 3,
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        ZhiFuBao = 4,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeiXin = 5,
        /// <summary>
        /// 无感支付
        /// </summary>
        [Description("无感支付")]
        WuGanZhiFu = 6
    }
    /// <summary>
    /// 车辆类型
    /// </summary>
    public enum VehicleTypeEnum
    {
        /// <summary>
        /// 其他车
        /// </summary>
        [Description("其他车")]
        Other = 0,
        /// <summary>
        /// 小型车
        /// </summary>
        [Description("小型车")]
        Small = 1,
        /// <summary>
        /// 大型车
        /// </summary>
        [Description("大型车")]
        Big = 2,
        /// <summary>
        /// 摩托车
        /// </summary>
        [Description("摩托车")]
        MoTuo = 3
    }
}