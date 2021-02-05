
namespace WmiCookBook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public int RecipeId { get; set; }
      
        public virtual Recipe Recipe { get; set; }
    }
}