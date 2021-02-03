using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Validators.Rules;
using FluentValidation;

namespace WmiCookBook.Validators.Auth
{
    public class AuthLoginRequestValidator : AbstractValidator<AuthLoginRequest>
    {
        public AuthLoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .SetValidator(new PasswordRule());
        }
    }
}