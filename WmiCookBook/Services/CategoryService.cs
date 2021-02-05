using WmiCookBook.Data;
using WmiCookBook.Services.Interfaces;
using System.Linq;

namespace WmiCookBook.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }
    }
}