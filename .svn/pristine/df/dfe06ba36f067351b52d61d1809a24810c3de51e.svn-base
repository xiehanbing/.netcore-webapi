using System.Collections.Generic;
using FluentValidation;
using General.Api.Application.User.Dto;

namespace General.Api.Application.User.Request
{
    /// <summary>
    /// 用户查询请求
    /// </summary>
    public class UserQuery:PageRequest
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public List<string> PersonIds  { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string  PersonName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public GenderType?   Gender { get; set; }
        /// <summary>
        /// 所属组织唯一标识码集合
        /// </summary>
        public List<string> OrgIndexCode { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public CertificateType? CertificateType { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string  CertificateNo { get; set; }
    }
    /// <summary>
    /// UserQueryValidator
    /// </summary>
    public class UserQueryValidator : AbstractValidator<UserQuery>
    {
        /// <summary>
        /// UserQueryValidator
        /// </summary>
        public UserQueryValidator()
        {
            //this.serviceProvider = serviceProvider;
            RuleFor(x => x.PageNo).Must(o => o > 0).WithMessage("PageNo is not null and must greater than 1").NotNull().WithMessage("PageNo is not null");
            RuleFor(x => x.PageSize).Must(o => o > 0).WithMessage("PageSize is not null and must greater than 1").NotNull().WithMessage("PageSize is not null");

            //RuleFor(x => x.Zip).Matches(@"^\d{5}(-?\d{4})?$");
            //RuleFor(x => x.State).MustAsync(IsKnownState).When(x => x.State != null);
            //RuleFor(x => x.State).MaximumLength(2);
        }
    }
}