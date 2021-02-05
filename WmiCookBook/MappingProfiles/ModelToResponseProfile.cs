using AutoMapper;
using WmiCookBook.Contracts.Response.Category;
using WmiCookBook.Contracts.Response.Ingredient;
using WmiCookBook.Contracts.Response.Recipe;
using WmiCookBook.Contracts.Response.Step;
using WmiCookBook.Contracts.Response.User;
using WmiCookBook.Models;

namespace WmiCookBook.MappingProfiles
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<Ingredient, IngredientResponse>();
            CreateMap<Step, StepResponse>();

            CreateMap<Recipe, RecipeResponse>();
            CreateMap<Recipe, RecipeFullResponse>();

            CreateMap<Category, CategoryResponse>();
        }
    }
}