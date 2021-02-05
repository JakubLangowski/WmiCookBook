using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WmiCookBook.Contracts;
using WmiCookBook.Contracts.Request;
using WmiCookBook.Contracts.Request.Recipe;
using WmiCookBook.Contracts.Response;
using WmiCookBook.Contracts.Response.Recipe;
using WmiCookBook.IntegrationTests.Factories;
using WmiCookBook.Models;
using Xunit;

namespace WmiCookBook.IntegrationTests.Tests
{
    public class RecipeControllerTest : IntegrationTestCore
    {
        [Fact]
        public async Task Controller___User_Cant_Access_Endpoints()
        {
            var response = await Client.DeleteAsync(ApiRoutes.Recipe.Delete.Replace("{recipeId}", "1"));
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            response = await Client.PatchAsync(ApiRoutes.Recipe.AddToFeatured.Replace("{recipeId}", "1"), content);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        [Fact]
        public async Task GetAll__User_Can_Retrieve_All_Accepted_Recipes()
        {
            await CreateRecipe(true);            
            await CreateRecipe(true);            
            await CreateRecipe(false);            
            
            var response = await Client.GetAsync(ApiRoutes.Recipe.GetAll);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<PagedResponse<RecipeResponse>>();
            body.Data.Should().NotBeEmpty();
            body.Data.Should().HaveCountGreaterOrEqualTo(2);
            
            body.Meta.Should().NotBeNull();
            body.Meta.PageNumber.Should().Be(1);
            body.Meta.Total.Should().Be(2);
            body.Meta.TotalPages.Should().Be(1);
        }
        
        [Fact]
        public async Task GetAll__User_Cant_Retrieve_List_Of_Not_Accepted_Recipe()
        {
            await CreateRecipe(false);            
            await CreateRecipe(false);            

            var response = await Client.GetAsync(ApiRoutes.Recipe.GetAll);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<PagedResponse<RecipeResponse>>();
            body.Data.Should().BeEmpty();
            body.Data.Should().HaveCount(0);
            
            body.Meta.Should().NotBeNull();
            body.Meta.PageNumber.Should().Be(0);
            body.Meta.Total.Should().Be(0);
            body.Meta.TotalPages.Should().Be(0);
        }
        
        [Fact]
        public async Task GetAll__Admin_Can_Retrieve_List_Of_Not_Accepted_Recipe()
        {
            await LogInAs("admin@gmail.com");
            await CreateRecipe(false);            
            await CreateRecipe(false);            

            var response = await Client.GetAsync(ApiRoutes.Recipe.GetAll);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<PagedResponse<RecipeResponse>>();
            body.Data.Should().NotBeEmpty();
            body.Data.Should().HaveCount(2);
            
            body.Meta.Should().NotBeNull();
            body.Meta.PageNumber.Should().Be(1);
            body.Meta.Total.Should().Be(2);
            body.Meta.TotalPages.Should().Be(1);
        }
        
        [Fact]
        public async Task GetAll__User_Can_Retrieve_4_Featured_Recipes()
        {
            for (int i = 0; i < 5; i++)
            {
                await CreateRecipe(true, true);
            }      
            
            var response = await Client.GetAsync(ApiRoutes.Recipe.GetFeatured);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<List<RecipeResponse>>();
            body.Should().NotBeEmpty();
            body.Should().HaveCount(4);
        }
        
        [Fact]
        public async Task GetAll__User_Can_Retrieve_8_New_Recipes()
        {
            for (int i = 0; i < 9; i++)
            {
                await CreateRecipe(true, true);
            }
            
            var response = await Client.GetAsync(ApiRoutes.Recipe.GetNew);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<List<RecipeResponse>>();
            body.Should().NotBeEmpty();
            body.Should().HaveCount(8);
        }
        
        [Fact]
        public async Task Get__User_Can_Retrieve_One_Accepted_Recipe()
        {
            Recipe createdRecipe = await CreateRecipe(true);

            var getEndpoint = ApiRoutes.Recipe.Get.Replace("{recipeId}", createdRecipe.Id.ToString());
            var response = await Client.GetAsync(getEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var responseData = await response.Content.ReadAsAsync<RecipeFullResponse>();
            
            responseData.Id.Should().Be(createdRecipe.Id);
            responseData.Name.Should().Be(createdRecipe.Name);
            responseData.Difficulty.Should().Be(createdRecipe.Difficulty);
            responseData.Time.Should().Be(createdRecipe.Time);
            responseData.Steps.Should().HaveCount(createdRecipe.Steps.Count);
            responseData.Ingredients.Should().HaveCount(createdRecipe.Ingredients.Count);
            responseData.Category.Id.Should().Be(createdRecipe.CategoryId);
        }
        
        [Fact]
        public async Task Get__User_Cant_Retrieve_Not_Accepted_Recipe()
        {
            Recipe createdRecipe = await CreateRecipe(false);

            var getEndpoint = ApiRoutes.Recipe.Get.Replace("{recipeId}", createdRecipe.Id.ToString());
            var response = await Client.GetAsync(getEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Get__Admin_Can_Retrieve_Not_Accepted_Recipe()
        {
            await LogInAs("admin@gmail.com");
            Recipe createdRecipe = await CreateRecipe(false);

            var getEndpoint = ApiRoutes.Recipe.Get.Replace("{recipeId}", createdRecipe.Id.ToString());
            var response = await Client.GetAsync(getEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create__User_Can_Create_Recipe()
        {
            Category category = await CreateCategory();
            CreateRecipeRequest recipeRequest = Factory.Recipe.GetCreateRecipeRequest(category.Id);
            
            var response = await Client.PostAsJsonAsync(ApiRoutes.Recipe.Create, recipeRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            
            var responseData = await response.Content.ReadAsAsync<RecipeFullResponse>();
            responseData.Id.Should().Be(1);
            responseData.Name.Should().Be(recipeRequest.Name);
            responseData.Difficulty.Should().Be(recipeRequest.Difficulty);
            responseData.Time.Should().Be(recipeRequest.Time);
            responseData.Steps.Should().HaveCount(recipeRequest.Steps.Count);
            responseData.Ingredients.Should().HaveCount(recipeRequest.Ingredients.Count);
            responseData.Category.Id.Should().Be(recipeRequest.CategoryId);
            
            int count = await Context.Recipes.AsNoTracking().CountAsync();
            count.Should().Be(1);
        }
        
        [Fact]
        public async Task Update__Admin__Can_Add_Recipe_To_Featured()
        {
            await LogInAs("admin@gmail.com");
            Recipe createdRecipe = await CreateRecipe(true, false);
            FeaturedRequest featuredRequest = new FeaturedRequest { Featured = true };
            
            HttpContent content = new StringContent(JsonSerializer.Serialize(featuredRequest), Encoding.UTF8, "application/json");
            string updateEndpoint = ApiRoutes.Recipe.AddToFeatured.Replace("{recipeId}", createdRecipe.Id.ToString());
            
            var response = await Client.PatchAsync(updateEndpoint, content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var updatedRecipe = await Context.Recipes
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == createdRecipe.Id);

            updatedRecipe.IsFeatured.Should().Be(featuredRequest.Featured);
        }
        
        [Fact]
        public async Task Delete__Admin_Can_Delete_Recipe()
        {
            await LogInAs("admin@gmail.com");
            Recipe createdRecipe = await CreateRecipe();
            string deleteEndpoint = ApiRoutes.Recipe.Delete.Replace("{recipeId}", createdRecipe.Id.ToString());
            
            var response = await Client.DeleteAsync(deleteEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            int count = await Context.Recipes.AsNoTracking().CountAsync();
            count.Should().Be(0);
        }

        private async Task<Recipe> CreateRecipe(bool isAccepted = true, bool isFeatured = false)
        {
            Recipe createdRecipe = Factory.Recipe.GetModel(isAccepted, isFeatured);
            await Context.Recipes.AddAsync(createdRecipe);
            await Context.SaveChangesAsync();
            return createdRecipe;
        }
        
        private async Task<Category> CreateCategory()
        {
            Category createdCategory = Factory.Category.GetModel();
            await Context.Categories.AddAsync(createdCategory);
            await Context.SaveChangesAsync();
            return createdCategory;
        }
    }
}