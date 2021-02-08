using FluentValidation;
using WmiCookBook.Contracts.Request.Category;

namespace WmiCookBook.Validators.Category
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Podaj nazwę kategorii")
                .MaximumLength(100).WithMessage("Nazwa nie może przekraczać 100 znaków");
            
            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Dodaj url zdjęcia")
                .MaximumLength(500).WithMessage("Url zdjęcia nie może przekraczać 500 znaków");
        }
    }
}