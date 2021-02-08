using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WmiCookBook.Helpers;
using WmiCookBook.Models;

namespace WmiCookBook.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IAuthHelper _authHelper;
        private readonly IWebHostEnvironment _environment;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IAuthHelper authHelper, IWebHostEnvironment environment) : base(options)
        {
            _authHelper = authHelper;
            _environment = environment;
        }
        
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Category>()
                .Property(x => x.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Category>()
                .Property(x => x.Image)
                .HasMaxLength(500);

            modelBuilder.Entity<Recipe>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Recipe>()
                .Property(x => x.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Recipe>()
                .Property(x => x.Image)
                .HasMaxLength(500);

            modelBuilder.Entity<Recipe>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingredient>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Ingredient>()
                .Property(x => x.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Ingredient>()
                .Property(x => x.Quantity)
                .HasMaxLength(100);
            modelBuilder.Entity<Ingredient>()
                .HasOne(x => x.Recipe)
                .WithMany(x => x.Ingredients)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Step>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Step>()
                .Property(x => x.Description)
                .HasMaxLength(1000);
            modelBuilder.Entity<Step>()
                .HasOne(x => x.Recipe)
                .WithMany(x => x.Steps)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
            
            Seeder.SeedUsers(modelBuilder, _authHelper, _environment);
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseLazyLoadingProxies();
        // }
    }
}