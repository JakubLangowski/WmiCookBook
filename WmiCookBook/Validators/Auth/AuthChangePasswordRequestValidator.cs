using WmiCookBook.Contracts.Request.Auth;
using FluentValidation;
using WmiCookBook.Validators.Rules;

namespace WmiCookBook.Validators.Auth
{
    public class AuthChangePasswordRequestValidator : AbstractValidator<AuthChangePasswordRequest>
    {
        public AuthChangePasswordRequestValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .SetValidator(new PasswordRule()); ;

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword);

        }
    }
}