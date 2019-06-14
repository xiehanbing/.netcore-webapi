using System;
using FluentValidation;

namespace General.Api.Application.Parking.Request.Record
{
    /// <summary>
    /// 查询车辆在车位上的停车信息 请求
    /// </summary>
    public class QueryParkRecordRequest : PageRequest
    {
        /// <summary>
        /// 查询标识
        /// </summary>
        public ParkRecordQueryType QueryType { get; set; }
        /// <summary>
        /// 停车库唯一标识
        /// </summary>
        public string ParkSysCode { get; set; }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 车位号
        /// </summary>
        public string SpaceNo { get; set; }
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

    }
    /// <summary>
    /// validator
    /// </summary>
    public class QueryParkRecordRequestValidator : AbstractValidator<QueryParkRecordRequest>
    {
        /// <summary>
        /// construct
        /// </summary>
        public QueryParkRecordRequestValidator()
        {
            RuleFor(x => x.PageNo).Must(o => o > 0).WithMessage("PageNo is not null and must greater than 1").NotNull().WithMessage("PageNo is not null");
            RuleFor(x => x.PageSize).Must(o => o > 0).WithMessage("PageSize is not null and must greater than 1").NotNull().WithMessage("PageSize is not null");
            RuleFor(x => x.QueryType).NotNull();
        }
    }
}