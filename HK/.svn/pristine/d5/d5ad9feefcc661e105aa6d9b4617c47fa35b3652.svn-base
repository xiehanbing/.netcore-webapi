using System;
using System.ComponentModel;
using FluentValidation;

namespace General.Api.Application.Parking.Request.Device
{
    /// <summary>
    /// 反控道闸 请求实体
    /// </summary>
    public class DeviceControlRequest
    {
        /// <summary>
        /// 车道唯一标识码
        /// </summary>
        public string RoadWaySysCode { get; set; }
        /// <summary>
        /// 控闸命令 0：关闸 1：开闸 3：常开
        /// </summary>
        public DeviceCommandType Command { get; set; }

    }
    /// <summary>
    /// validator
    /// </summary>
    public class DeviceControlRequestValidator : AbstractValidator<DeviceControlRequest>
    {
        /// <summary>
        /// construct
        /// </summary>
        public DeviceControlRequestValidator()
        {
            RuleFor(x => x.RoadWaySysCode).NotNull().WithMessage("RoadWaySysCode is not null");
            RuleFor(x => x.Command).NotNull().WithMessage("PageSize is not null");
        }
    }
    /// <summary>
    /// 控闸命令 类型
    /// </summary>
    public enum DeviceCommandType
    {
        /// <summary>
        /// 关闸
        /// </summary>
        [Description("关闸")]
        Close = 0,
        /// <summary>
        /// 开闸
        /// </summary>
        [Description("开闸")]
        Open = 1,
        /// <summary>
        /// 常开
        /// </summary>
        [Description("常开")]
        AwayOpen = 3
    }
}