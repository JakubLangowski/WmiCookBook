using FluentValidation;
using WmiCookBook.Contracts.Request;

namespace WmiCookBook.Validators
{
    public class FeaturedRequestValidator : AbstractValidator<FeaturedRequest>
    {
        public FeaturedRequestValidator()
        {
            RuleFor(x => x.Featured)
                .NotEmpty().Must(x => x == false || x == true);
        }
    }
}