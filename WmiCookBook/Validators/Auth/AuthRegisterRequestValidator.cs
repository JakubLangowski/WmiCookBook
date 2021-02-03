using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Validators.Rules;
using FluentValidation;

namespace WmiCookBook.Validators.Auth
{
    public class AuthRegisterRequestValidator : AbstractValidator<AuthRegisterRequest>
    {
        public AuthRegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .SetValidator(new PasswordRule());

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password).WithMessage("Podane hasła nie są identyczne");
        }
    }
}