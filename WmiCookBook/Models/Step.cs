using System.ComponentModel.DataAnnotations.Schema;

namespace WmiCookBook.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
        
        public virtual Recipe Recipe { get; set; }
    }
}