using System.Collections.Generic;

namespace WmiCookBook.Data
{
    public class ImportJson
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<SeedRecipeRequest> Recipes { get; set; }
    }
}