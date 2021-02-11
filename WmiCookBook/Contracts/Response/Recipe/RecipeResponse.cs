
using WmiCookBook.Contracts.Response.Category;

namespace WmiCookBook.Contracts.Response.Recipe
{
    public class RecipeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsFeatured { get; set; }
        public int Time { get; set; }
        public virtual CategoryResponse Category { get; set; }
    }
}