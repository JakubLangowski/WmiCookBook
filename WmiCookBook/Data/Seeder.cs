using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WmiCookBook.Helpers;
using WmiCookBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WmiCookBook.Contracts.Request.Ingredient;
using WmiCookBook.Contracts.Request.Recipe;
using WmiCookBook.Contracts.Request.Step;

namespace WmiCookBook.Data
{
    public static class Seeder
    {
        public static void SeedUsers(ModelBuilder modelBuilder, IAuthHelper authHelper, IWebHostEnvironment env)
        {
            authHelper.CreatePasswordHash("Password#2!", out byte[] passwordHash, out byte[] passwordSalt);



            User adminUser = new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            
            User user = new User
            {
                Id = 2,
                Email = "user@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            
            modelBuilder.Entity<User>().HasData(
                adminUser,
                user
            );

            if (!env.IsEnvironment("Test"))
            {
                var data = SeedRecipes(env);

                modelBuilder.Entity<Category>().HasData(
                    data.Item1
                );

                modelBuilder.Entity<Recipe>().HasData(
                    data.Item2
                );

                modelBuilder.Entity<Ingredient>().HasData(
                    data.Item3
                );

                modelBuilder.Entity<Step>().HasData(
                    data.Item4
                );
            }
        }

        private static (List<Category>, List<Recipe>, List<Ingredient>,  List<Step>) SeedRecipes(IWebHostEnvironment env)
        {
            
            var jsonString = File.ReadAllText(env.ContentRootPath + "/Data/data.json");
                List<ImportJson> importJsons = JsonConvert.DeserializeObject<List<ImportJson>>(jsonString);

                List<Category> categories = new List<Category>();
                List<Recipe> recipes = new List<Recipe>();
                List<Ingredient> ingredients = new List<Ingredient>();
                List<Step> steps = new List<Step>();

                int categoryCounter = 1;
                int recipeCounter = 1;
                int ingredientsCounter = 1;
                int stepsCounter = 1;

                foreach (ImportJson importJson in importJsons)
                {
                    categories.Add(new Category
                    {
                        Id = categoryCounter,
                        Image = importJson.Image,
                        Name = importJson.Name
                    });
                    foreach (SeedRecipeRequest recipeRequest in importJson.Recipes)
                    {
                        recipes.Add(new Recipe
                        {
                            Id = recipeCounter,
                            CategoryId = categoryCounter,
                            Name = recipeRequest.Name,
                            Image = recipeRequest.Image,
                            Difficulty = recipeRequest.Difficulty,
                            Time = recipeRequest.Time,
                            IsAccepted = true,
                        });
                        foreach (CreateIngredientRequest ingredientRequest in recipeRequest.Ingredients)
                        {
                            ingredients.Add(new Ingredient
                            {
                                Id = ingredientsCounter,
                                Name = ingredientRequest.Name,
                                Quantity = ingredientRequest.Quantity,
                                RecipeId = recipeCounter
                            });
                            ingredientsCounter++;
                        }

                        foreach (CreateStepRequest stepRequest in recipeRequest.Steps)
                        {
                            steps.Add(new Step
                            {
                                Id = stepsCounter,
                                Description = stepRequest.Description,
                                RecipeId = recipeCounter
                            });
                            stepsCounter++;
                        }

                        recipeCounter++;
                    }

                    categoryCounter++;
                }

                return (categories, recipes, ingredients, steps);
        }
    }
}