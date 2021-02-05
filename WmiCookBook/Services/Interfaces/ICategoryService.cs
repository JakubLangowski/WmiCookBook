using System.Collections.Generic;
using System.Threading.Tasks;
using WmiCookBook.Contracts.Request;
using WmiCookBook.Models;

namespace WmiCookBook.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Category>> GetFeaturedCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<bool> AddCategoryToFeaturedAsync(Category category, FeaturedRequest featuredRequest);
        Task<bool> DeleteCategoryAsync(Category category);
        bool CategoryExists(int id);
    }
}