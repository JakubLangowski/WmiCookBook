using WmiCookBook.Helpers;
using WmiCookBook.Models;
using Microsoft.EntityFrameworkCore;

namespace WmiCookBook.Data
{
    public static class Seeder
    {
        public static void SeedUsers(ModelBuilder modelBuilder, IAuthHelper authHelper)
        {
            authHelper.CreatePasswordHash("Password#2!", out byte[] passwordHash, out byte[] passwordSalt);

            User adminUser = new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            User moderatorUser = new User
            {
                Id = 2,
                Email = "moderator@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            User user = new User
            {
                Id = 3,
                Email = "user@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            modelBuilder.Entity<User>().HasData(
                adminUser,
                moderatorUser,
                user
            );
        }
    }
}