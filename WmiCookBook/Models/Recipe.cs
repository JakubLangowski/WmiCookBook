using System;
using System.Collections.Generic;

namespace WmiCookBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public int Time { get; set; }
        public bool IsAccepted { get; set; } = false;
        public bool IsFeatured { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}