﻿using FluentValidation;
using General.Core;

namespace General.Api.Application
{
    /// <summary>
    /// 页面请求实体
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int  PageNo { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int  PageSize { get; set; }
    }
    /// <summary>
    /// TokenRequestValidator
    /// </summary>
    public class PageValidator : AbstractValidator<PageRequest>
    {
        //private readonly IServiceProvider serviceProvider;//IServiceProvider serviceProvider
        /// <summary>
        /// TokenRequestValidator
        /// </summary>
        public PageValidator()
        {
            //this.serviceProvider = serviceProvider;
            RuleFor(x => x.PageNo).NotEmpty().Must(o=>o>0).WithMessage("PageNo is not null and must greater than 1");
            RuleFor(x => x.PageSize).NotEmpty().Must(o=>o>0).WithMessage("PageSize is not null and must greater than 1");

            //RuleFor(x => x.Zip).Matches(@"^\d{5}(-?\d{4})?$");
            //RuleFor(x => x.State).MustAsync(IsKnownState).When(x => x.State != null);
            //RuleFor(x => x.State).MaximumLength(2);
        }
    }
}