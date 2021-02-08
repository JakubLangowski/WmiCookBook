using System.Collections.Generic;

namespace WmiCookBook.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        
        // TODO Zmienić default na false
        public bool IsFeatured { get; set; } = true;
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}