using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;

namespace General.Api.Application.Door.Request
{
    /// <summary>
    /// 查询门禁点事件
    /// </summary>
    public class DoorEventQueryRequest:PageRequest
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public int? EventType { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string  PersonName { get; set; }
        /// <summary>
        /// 人员Id数组
        /// </summary>
        public List<string> PersonIds { get; set; }
        /// <summary>
        /// 门禁点名称
        /// </summary>
        public string  DoorName { get; set; }
        /// <summary>
        /// 门禁点唯一标识数组
        /// </summary>
        public List<string> DoorIndexCodes { get; set; }
        /// <summary>
        /// 门禁点所在的区域唯一编号
        /// </summary>
        public string  DoorRegionIndexCode { get; set; }
    }
    /// <summary>
    /// validator
    /// </summary>
    public class DoorEventQueryRequestValidator : AbstractValidator<DoorEventQueryRequest>
    {
        /// <summary>
        /// construct
        /// </summary>
        public DoorEventQueryRequestValidator()
        {
            RuleFor(x => x.PageNo).Must(o => o > 0).WithMessage("PageNo is not null and must greater than 1").NotNull().WithMessage("PageNo is not null");
            RuleFor(x => x.PageSize).Must(o => o > 0).WithMessage("PageSize is not null and must greater than 1").NotNull().WithMessage("PageSize is not null");
            RuleFor(x => x.StartTime).Must(x => !x.Equals(DateTime.MinValue))
                .WithMessage("StartTime can't be minvalue");
            RuleFor(x => x.EndTime).Must(x => !x.Equals(DateTime.MinValue))
                .WithMessage("EndTime can't be minvalue");
        }
    }
}