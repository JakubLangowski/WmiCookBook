namespace WmiCookBook.Models.Filters
{
    public class RecipeFilter
    {
        public int?[] CategoryId { get; set; }
        public bool? Featured { get; set; }
    }
}