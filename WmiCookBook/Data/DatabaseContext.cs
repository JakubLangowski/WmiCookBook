using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            Seeder.SeedUsers(modelBuilder, _authHelper);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}