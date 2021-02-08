using FluentValidation;
using WmiCookBook.Contracts.Request.Recipe;
using WmiCookBook.Services.Interfaces;

namespace WmiCookBook.Validators.Recipe
{
    public class CreateRecipeRequestValidator : AbstractValidator<CreateRecipeRequest>
    {
        public CreateRecipeRequestValidator(ICategoryService categoryService)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Podaj nazwę przepisu")
                .MaximumLength(100).WithMessage("Nazwa nie może przekraczać 100 znaków");
            
            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Dodaj url zdjęcia")
                .MaximumLength(500).WithMessage("Url zdjęcia nie może przekraczać 500 znaków");
            
            RuleFor(x => x.Difficulty)
                .InclusiveBetween(1, 3).WithMessage("Podaj poprawny poziom trudności");
            
            RuleFor(x => x.Time)
                .InclusiveBetween(1, 1000).WithMessage("Podaj poprawny czas");
            
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Podaj poprwaną kategorię")
                .DependentRules(() =>
                {
                    RuleFor(x => x.CategoryId)
                        .Must(categoryId => categoryService.CategoryExists(categoryId))
                        .WithMessage("Podana kategoria nie istnieje");
                });

            RuleForEach(x => x.Ingredients)
                .ChildRules(ingredient =>
                {
                    ingredient.RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("Składnik musi mieć nazwę")
                        .MaximumLength(100).WithMessage("Nazwa nie może przekraczać 100 znaków");
                    ingredient.RuleFor(x => x.Quantity)
                        .NotEmpty().WithMessage("Składnik musi mieć ilość")
                        .MaximumLength(100).WithMessage("Ilość nie może przekraczać 100 znaków");
                });
            
            RuleForEach(x => x.Steps)
                .ChildRules(ingredient =>
                {
                    ingredient.RuleFor(x => x.Description)
                        .NotEmpty().WithMessage("Krok nie może być pusty")
                        .MaximumLength(100).WithMessage("Kron nie może przekraczać 1000 znaków");
                });
        }
    }
}