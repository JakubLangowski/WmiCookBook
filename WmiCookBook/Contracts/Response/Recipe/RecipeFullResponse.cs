using System.Collections.Generic;
using WmiCookBook.Contracts.Response.Category;
using WmiCookBook.Contracts.Response.Ingredient;
using WmiCookBook.Contracts.Response.Step;

namespace WmiCookBook.Contracts.Response.Recipe
{
    public class RecipeFullResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public int Time { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFeatured { get; set; }
        public virtual ICollection<IngredientResponse> Ingredients { get; set; }
        public virtual ICollection<StepResponse> Steps { get; set; }
        public virtual CategoryResponse Category { get; set; }
    }
}