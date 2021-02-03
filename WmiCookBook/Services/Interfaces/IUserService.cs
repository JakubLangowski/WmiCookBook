using WmiCookBook.Models;
using WmiCookBook.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WmiCookBook.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync(PaginationFilter paginationFilter);
        Task<User> GetUserByIdAsync(int id);
        Task<int> CountUsersAsync();
    }
}