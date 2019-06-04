using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace General.Core
{
    /// <summary>
    /// token请求类
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
    /// <summary>
    /// TokenRequestValidator
    /// </summary>
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        //private readonly IServiceProvider serviceProvider;//IServiceProvider serviceProvider
        /// <summary>
        /// TokenRequestValidator
        /// </summary>
        public TokenRequestValidator()
        {
            //this.serviceProvider = serviceProvider;
            RuleFor(x => x.Account).NotEmpty().WithMessage("Account is not null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is not null");

            //RuleFor(x => x.Zip).Matches(@"^\d{5}(-?\d{4})?$");
            //RuleFor(x => x.State).MustAsync(IsKnownState).When(x => x.State != null);
            //RuleFor(x => x.State).MaximumLength(2);
        }
        //private async Task<bool> IsKnownState(string abbreviation, CancellationToken token)
        //{
        //    var stateRepository = serviceProvider.GetRequiredService<IStateRepository>();
        //    var state = await stateRepository.GetStateAsync(abbreviation, token);
        //    return state != null;
        //}
    }
}