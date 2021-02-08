using System.Collections.Generic;
using WmiCookBook.Contracts.Request.Recipe;

namespace WmiCookBook.Data
{
    public class ImportJson
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<CreateRecipeRequest> Recipes { get; set; }
    }
}