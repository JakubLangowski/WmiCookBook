using System.Threading.Tasks;
using WmiCookBook.Data;
using WmiCookBook.Models;

namespace WmiCookBook.Helpers
{
    public interface IAuthHelper
    {
        public Task<User> GetAuthenticatedUserModel(DatabaseContext context);
        public int GetAuthenticatedUserId();
        public string GetAuthenticatedUserRole();
        bool IsNormalUser();
        bool IsAdmin();
        bool IsEditor();
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}