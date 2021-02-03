using WmiCookBook.Contracts.Request.Auth;
using FluentValidation;

namespace WmiCookBook.Validators.Auth
{
    public class AuthRefreshTokenRequestValidator : AbstractValidator<AuthRefreshTokenRequest>
    {
        public AuthRefreshTokenRequestValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();

            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}