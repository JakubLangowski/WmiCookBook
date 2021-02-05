using AutoMapper;
using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Contracts.Request.Category;
using WmiCookBook.Contracts.Request.Ingredient;
using WmiCookBook.Contracts.Request.Queries;
using WmiCookBook.Contracts.Request.Recipe;
using WmiCookBook.Contracts.Request.Step;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;

namespace WmiCookBook.MappingProfiles
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            
            CreateMap<AuthRegisterRequest, User>();
            CreateMap<AuthChangePasswordRequest, User>();

            CreateMap<RecipeQuery, RecipeFilter>();
            CreateMap<CreateRecipeRequest, Recipe>();

            CreateMap<AddIngredientRequest, Ingredient>();
            CreateMap<AddStepRequest, Step>();

            CreateMap<CreateCategoryRequest, Category>();
        }
    }
}