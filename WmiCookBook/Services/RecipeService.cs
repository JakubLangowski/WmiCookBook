using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WmiCookBook.Contracts.Request;
using WmiCookBook.Data;
using WmiCookBook.Helpers;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;
using WmiCookBook.Services.Interfaces;

namespace WmiCookBook.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly DatabaseContext _context;
        private readonly IAuthHelper _authHelper;
        private readonly IImageService _imageService;

        public RecipeService(DatabaseContext context, IAuthHelper authHelper, IImageService imageService)
        {
            _context = context;
            _authHelper = authHelper;
            _imageService = imageService;
        }

        public async Task<List<Recipe>> GetAllRecipeAsync(PaginationFilter paginationFilter, RecipeFilter recipeFilter)
        {
            var queryable = _context.Recipes
                .Where(x => x.IsAccepted)
                .AsQueryable();
            
            queryable = FilteredRecipes(queryable, recipeFilter);
            
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            
            return await queryable
                .Include(x => x.Category)
                .Skip(skip).Take(paginationFilter.PageSize)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
        
        public async Task<List<Recipe>> GetAllNotAcceptedRecipeAsync(PaginationFilter paginationFilter)
        {
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await _context.Recipes
                .Where(x => !x.IsAccepted)
                .Include(x => x.Category)
                .Skip(skip).Take(paginationFilter.PageSize)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetFeaturedRecipeAsync()
        {
            return await _context.Recipes
                .Where(x => x.IsAccepted)
                .Where(x => x.IsFeatured)
                .Take(4)
                .ToListAsync();
        }

        public async Task<List<Recipe>> GetNewRecipeAsync()
        {
            return await _context.Recipes
                .Where(x => x.IsAccepted)
                .OrderByDescending(x => x.CreatedAt)
                .Take(8)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var queryable = _context.Recipes.AsQueryable();
            queryable = FilterAccepted(queryable);
            
            return await queryable
                .Include(x => x.Ingredients)
                .Include(x => x.Steps)
                .Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe, IFormFile formFile)
        {
            string fileName = await _imageService.UploadFile(formFile);
            recipe.Image = fileName;
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            await _context.Entry(recipe)
                .Reference(x => x.Category)
                .LoadAsync();
            return recipe;
        }

        public async Task<bool> AcceptRecipeAsync(Recipe recipe)
        {
            recipe.IsAccepted = true;
            _context.Recipes.Update(recipe);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> AddRecipeToFeaturedAsync(Recipe recipe, FeaturedRequest featuredRequest)
        {
            recipe.IsFeatured = featuredRequest.Featured;
            _context.Recipes.Update(recipe);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<int> RecipeCountAsync(RecipeFilter recipeFilter)
        {
            var queryable = _context.Recipes
                .Where(x => x.IsAccepted)
                .AsQueryable();
            queryable = FilteredRecipes(queryable, recipeFilter);
            return await queryable.CountAsync();
        }
        
        public async Task<int> RecipeCountNotAcceptedAsync()
        {
            return await _context.Recipes.Where(x => !x.IsAccepted).CountAsync();
        }
        
        private IQueryable<Recipe> FilteredRecipes(IQueryable<Recipe> queryable, RecipeFilter recipeFilter)
        {
            if (recipeFilter.CategoryId != null && recipeFilter.CategoryId.Length > 0)
                queryable = queryable.Where(x => recipeFilter.CategoryId.Contains(x.CategoryId));
            if (recipeFilter.Featured != null)
                queryable = queryable.Where(x => x.IsFeatured == recipeFilter.Featured);
            
            return queryable;
        }
        
        private IQueryable<Recipe> FilterAccepted(IQueryable<Recipe> queryable)
        {
            if (_authHelper.GetAuthenticatedUserId() == 0)
            {
                queryable = queryable.Where(x => x.IsAccepted);
            }
            return queryable;
        }
    }
}