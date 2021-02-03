using WmiCookBook.Data;
using WmiCookBook.Helpers;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;
using WmiCookBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WmiCookBook.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IAuthHelper _authHelper;

        public UserService(DatabaseContext context, IAuthHelper authHelper)
        {
            _context = context;
            _authHelper = authHelper;
        }

        public async Task<List<User>> GetAllUsersAsync(PaginationFilter paginationFilter)
        {
            var queryable = _context.Users.AsQueryable();

            int skip = PaginationHelper.CountSkip(paginationFilter);

            return await queryable
                .Skip(skip).Take(paginationFilter.PageSize)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var queryable = _context.Users.AsQueryable();

            if (_authHelper.IsNormalUser())
                queryable = queryable.Where(x => x.Id == _authHelper.GetAuthenticatedUserId());

            return await queryable.FirstOrDefaultAsync(x => x.Id == id);
        }
        

        public async Task<int> CountUsersAsync()
        {
            return await _context.Users.CountAsync();
        }
    }
}