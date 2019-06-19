using FluentValidation;

namespace General.Api.Application.Token
{
    /// <summary>
    /// ApiAuthUserDto
    /// </summary>
    public class ApiAuthUserDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string  Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string  Password { get; set; }
    }
    /// <summary>
    /// ApiAuthUserDtoValidator
    /// </summary>
    public class ApiAuthUserDtoValidator : AbstractValidator<ApiAuthUserDto>
    {
        /// <summary>
        /// UserQueryValidator
        /// </summary>
        public ApiAuthUserDtoValidator()
        {
            //this.serviceProvider = serviceProvider;
            RuleFor(x => x.Account).NotNull().WithMessage("Account is not null");
            RuleFor(x => x.Password).NotNull().WithMessage("Password is not null");

            //RuleFor(x => x.Zip).Matches(@"^\d{5}(-?\d{4})?$");
            //RuleFor(x => x.State).MustAsync(IsKnownState).When(x => x.State != null);
            //RuleFor(x => x.State).MaximumLength(2);
        }
    }
}