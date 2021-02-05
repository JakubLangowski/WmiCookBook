using System.Collections.Generic;
using WmiCookBook.Data;
using WmiCookBook.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WmiCookBook.Contracts.Request;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;

namespace WmiCookBook.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetFeaturedCategoriesAsync()
        {
            return await _context.Categories
                .Where(x => x.IsFeatured)
                .Take(4)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            
            return category;
        }

        public async Task<bool> AddCategoryToFeaturedAsync(Category category, FeaturedRequest featuredRequest)
        {
            category.IsFeatured = featuredRequest.Featured;
            _context.Categories.Update(category);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }
    }
}