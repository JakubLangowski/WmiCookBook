using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace WmiCookBook.Models
{

    public class Model : DbContext
    {

        public Model(DbContextOptions<Model> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Step>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Step>()
                .Property(x => x.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Category>()
                .HasMany(x => x.Recipes)
                //.WithOne(x => x.Category)
                .WithOne()
                .HasForeignKey(x => x.CategoryId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<Recipe>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.Ingredients)
                //.WithOne(x => x.Recipe)
                .WithOne()
                .HasForeignKey(x => x.RecipeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.Steps)
                //.WithOne(x => x.Recipe)
                .WithOne()
                .HasForeignKey(x => x.RecipeId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Recipe>()
                .Property(x => x.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Recipe>()
                .Property(x => x.Image)
                .HasMaxLength(500);

            modelBuilder.Entity<Ingredient>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Ingredient>()
                .Property(x => x.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Ingredient>()
                .Property(x => x.Quantity)
                .HasMaxLength(100);

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Step> Steps { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

    }

    public class Step
    {

        public int Id { get; set; }
        public string Description { get; set; }
        //public Recipe Recipe { get; set; }
        public int RecipeId { get; set; }

    }

    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }

    }

    public class Recipe
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Difficulty { get; set; }
        public int Time { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        //public Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<Step> Steps { get; set; }

    }

    public class Ingredient
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public int RecipeId { get; set; }
        //public Recipe Recipe { get; set; }

    }

}
