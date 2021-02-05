using System;
using System.Collections.Generic;
using WmiCookBook.Contracts.Request.Ingredient;
using WmiCookBook.Contracts.Request.Step;

namespace WmiCookBook.Contracts.Request.Recipe
{
    public class CreateRecipeRequest
    {
        public string Name { get; set; }
        public int Difficulty { get; set; }
        public int Time { get; set; }
        public virtual ICollection<AddIngredientRequest> Ingredients { get; set; }
        public virtual ICollection<AddStepRequest> Steps { get; set; }
        public int CategoryId { get; set; }
    }
}