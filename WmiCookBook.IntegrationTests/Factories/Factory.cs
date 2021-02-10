using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bogus;
using Microsoft.AspNetCore.Http;
using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Contracts.Request.Category;
using WmiCookBook.Contracts.Request.Ingredient;
using WmiCookBook.Contracts.Request.Recipe;
using WmiCookBook.Contracts.Request.Step;
using WmiCookBook.Helpers;
using WmiCookBook.Models;

namespace WmiCookBook.IntegrationTests.Factories
{
    public static class Factory
    {

        public static class UserFactory
        {
            public static User GetModel()
            {

                var authHelper = new AuthHelper();
                authHelper.CreatePasswordHash("Password#2!", out byte[] passwordHash, out byte[] passwordSalt);
                return new Faker<User>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.PasswordHash, passwordHash)
                    .RuleFor(x => x.PasswordSalt, passwordSalt)
                    .Generate();
            }
        }

        public static class Auth
        {
            public static AuthLoginRequest AuthLoginRequest(string email = "", string password = "")
            {
                var fakeData = new Faker<AuthLoginRequest>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.Password, "Password#2!")
                    .Generate();
                if (email != "")
                    fakeData.Email = email;
                if (password != "")
                    fakeData.Password = password;
                return fakeData;
            }

            public static AuthRegisterRequest AuthRegisterRequest()
            {
                return new Faker<AuthRegisterRequest>()
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.Password, "Password#2!")
                    .RuleFor(x => x.ConfirmPassword, "Password#2!")
                    .Generate();
            }

            public static AuthChangePasswordRequest AuthChangePasswordRequest()
            {
                return new Faker<AuthChangePasswordRequest>()
                    .RuleFor(x => x.OldPassword, "Password#2!")
                    .RuleFor(x => x.NewPassword, "NewPassword#2!")
                    .RuleFor(x => x.ConfirmNewPassword, "NewPassword#2!")
                    .Generate();
            }
        }
        
        public class Recipe
        {
            public static Models.Recipe GetModel(bool isAccepted = true, bool isFeatured = false)
            {
                return new Faker<Models.Recipe>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Image, f => f.Lorem.Word())
                    .RuleFor(x => x.Time, f => f.Random.Number(1, 1000))
                    .RuleFor(x => x.Difficulty, f => f.Random.Number(1, 3))
                    .RuleFor(x => x.Steps, Step.GetModels())
                    .RuleFor(x => x.Ingredients, Ingredient.GetModels())
                    .RuleFor(x => x.IsFeatured, isFeatured)
                    .RuleFor(x => x.IsAccepted, isAccepted)
                    .RuleFor(x => x.Category, Category.GetModel());
            }
            
            // public static CreateRecipeRequest GetCreateRecipeRequest(int categoryId)
            // {
            //     IFormFile file = new FormFile(
            //         new MemoryStream(
            //             Convert.FromBase64String(ImageFactory.GetTestImage())), 0, 0, "Data", "dummy.jpg");
            //     return new Faker<CreateRecipeRequest>()
            //         .RuleFor(x => x.Name, f => f.Lorem.Word())
            //         .RuleFor(x => x.Image, file)
            //         .RuleFor(x => x.Time, f => f.Random.Number(1, 1000))
            //         .RuleFor(x => x.Difficulty, f => f.Random.Number(1, 3))
            //         .RuleFor(x => x.Steps, Step.GetCreateStepRequests())
            //         .RuleFor(x => x.Ingredients, Ingredient.GetCreateIngredientRequests())
            //         .RuleFor(x => x.CategoryId, categoryId);
            // }
        }
        
        public class Category
        {
            public static Models.Category GetModel(bool isFeatured = false)
            {
                return new Faker<Models.Category>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Image, f => f.Internet.Url())
                    .RuleFor(x => x.IsFeatured, isFeatured)
                    .Generate();
            }
            
            public static CreateCategoryRequest GetCreateCategoryRequest()
            {
                return new Faker<CreateCategoryRequest>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Image, f => f.Internet.Url())
                    .Generate();
            }
        }
        
        public class Ingredient
        {
            public static Models.Ingredient GetModel()
            {
                return new Faker<Models.Ingredient>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Quantity, f => f.Lorem.Word())
                    .Generate();
            }
            
            public static CreateIngredientRequest GetCreateIngredientRequest()
            {
                return new Faker<CreateIngredientRequest>()
                    .RuleFor(x => x.Name, f => f.Lorem.Word())
                    .RuleFor(x => x.Quantity, f => f.Lorem.Word())
                    .Generate();
            }
            
            public static List<Models.Ingredient> GetModels(int qty = 1)
            {
                List<Models.Ingredient> ingredients = new List<Models.Ingredient>();
                for (int i = 0; i < qty; i++)
                {
                    ingredients.Add(GetModel());
                }
                return ingredients;
            }
            
            public static List<CreateIngredientRequest> GetCreateIngredientRequests(int qty = 1)
            {
                List<CreateIngredientRequest> ingredients = new List<CreateIngredientRequest>();
                for (int i = 0; i < qty; i++)
                {
                    ingredients.Add(GetCreateIngredientRequest());
                }
                return ingredients;
            }
        }
        
        public class Step
        {
            public static Models.Step GetModel()
            {
                return new Faker<Models.Step>()
                    .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                    .Generate();
            }
            
            public static List<Models.Step> GetModels(int qty = 1)
            {
                List<Models.Step> ingredients = new List<Models.Step>();
                for (int i = 0; i < qty; i++)
                {
                    ingredients.Add(GetModel());
                }
                return ingredients;
            }
            
            public static CreateStepRequest GetCreateStepRequest()
            {
                return new Faker<CreateStepRequest>()
                    .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                    .Generate();
            }
            
            public static List<CreateStepRequest> GetCreateStepRequests(int qty = 1)
            {
                List<CreateStepRequest> ingredients = new List<CreateStepRequest>();
                for (int i = 0; i < qty; i++)
                {
                    ingredients.Add(GetCreateStepRequest());
                }
                return ingredients;
            }
        }
    }
}