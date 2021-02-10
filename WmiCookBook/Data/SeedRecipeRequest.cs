using System.Collections.Generic;
using WmiCookBook.Contracts.Request.Ingredient;
using WmiCookBook.Contracts.Request.Step;

namespace WmiCookBook.Data
{
    public class SeedRecipeRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public int Time { get; set; }
        public virtual ICollection<CreateIngredientRequest> Ingredients { get; set; }
        public virtual ICollection<CreateStepRequest> Steps { get; set; }
        public int CategoryId { get; set; }
    }
}