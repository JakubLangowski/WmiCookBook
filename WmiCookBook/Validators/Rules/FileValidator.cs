using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace WmiCookBook.Validators.Rules
{
    public class FileValidator  : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(150000)
                .WithMessage("Zdjęcie musi ważyć mniej niż 150kb");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("Zły format zdjęcia");
        }
    }
}