namespace WmiCookBook.Contracts.Request.Recipe
{
    public class RecipeQuery
    {
        public int?[] CategoryId { get; set; }
        public bool? Featured { get; set; }
    }
}