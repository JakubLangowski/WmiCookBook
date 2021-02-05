using System.Collections.Generic;
using System.Threading.Tasks;
using WmiCookBook.Contracts.Request;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;

namespace WmiCookBook.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipeAsync(PaginationFilter paginationFilter, RecipeFilter recipeFilter);
        Task<List<Recipe>> GetFeaturedRecipeAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task<bool> AddRecipeToFeaturedAsync(Recipe recipe, FeaturedRequest featuredRequest);
        Task<bool> DeleteRecipeAsync(Recipe recipe);
        Task<int> RecipeCountAsync(RecipeFilter recipeFilter);
    }
}