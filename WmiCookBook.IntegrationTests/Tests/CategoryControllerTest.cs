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
using WmiCookBook.Contracts.Request.Category;
using WmiCookBook.Contracts.Response.Category;
using WmiCookBook.IntegrationTests.Factories;
using WmiCookBook.Models;
using Xunit;

namespace WmiCookBook.IntegrationTests.Tests
{
    public class CategoryControllerTest : IntegrationTestCore
    {
        [Fact]
        public async Task Controller___User_Cant_Access_Endpoints()
        {
            var response = await Client.DeleteAsync(ApiRoutes.Category.Delete.Replace("{categoryId}", "1"));
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            response = await Client.PostAsync(ApiRoutes.Category.Create, content);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            response = await Client.PatchAsync(ApiRoutes.Category.AddToFeatured.Replace("{categoryId}", "1"), content);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        [Fact]
        public async Task GetAll__User_Can_Retrieve_All_Categories()
        {
            await CreateCategory();            
            await CreateCategory();

            var response = await Client.GetAsync(ApiRoutes.Category.GetAll);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<List<CategoryResponse>>();
            body.Should().NotBeEmpty();
            body.Should().HaveCountGreaterOrEqualTo(2);
        }
        
        [Fact]
        public async Task GetAll__User_Can_Retrieve_4_Featured_Categories()
        {
            for (int i = 0; i < 5; i++)
            {
                await CreateCategory(true);
            }      
            
            var response = await Client.GetAsync(ApiRoutes.Category.GetFeatured);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var body = await response.Content.ReadAsAsync<List<CategoryResponse>>();
            body.Should().NotBeEmpty();
            body.Should().HaveCount(4);
        }
        
        [Fact]
        public async Task Get__User_Can_Retrieve_One_Category()
        {
            Category createdCategory = await CreateCategory();

            var getEndpoint = ApiRoutes.Category.Get.Replace("{categoryId}", createdCategory.Id.ToString());
            var response = await Client.GetAsync(getEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var responseData = await response.Content.ReadAsAsync<CategoryResponse>();
            
            responseData.Id.Should().Be(createdCategory.Id);
            responseData.Name.Should().Be(createdCategory.Name);
        }
        
        [Fact]
        public async Task Create__Admin_Can_Create_Category()
        {
            await LogInAs("admin@gmail.com");
            CreateCategoryRequest categoryRequest = Factory.Category.GetCreateCategoryRequest();
            
            var response = await Client.PostAsJsonAsync(ApiRoutes.Category.Create, categoryRequest);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            
            var responseData = await response.Content.ReadAsAsync<CategoryResponse>();
            responseData.Id.Should().Be(1);
            responseData.Name.Should().Be(categoryRequest.Name);

            int count = await Context.Categories.AsNoTracking().CountAsync();
            count.Should().Be(1);
        }
        
        [Fact]
        public async Task Update__Admin__Can_Add_Category_To_Featured()
        {
            await LogInAs("admin@gmail.com");
            Category createdCategory = await CreateCategory();
            FeaturedRequest featuredRequest = new FeaturedRequest { Featured = true };
            
            HttpContent content = new StringContent(JsonSerializer.Serialize(featuredRequest), Encoding.UTF8, "application/json");
            string updateEndpoint = ApiRoutes.Category.AddToFeatured.Replace("{categoryId}", createdCategory.Id.ToString());
            
            var response = await Client.PatchAsync(updateEndpoint, content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var updatedCategory = await Context.Categories
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == createdCategory.Id);

            updatedCategory.IsFeatured.Should().Be(featuredRequest.Featured);
        }
        
        [Fact]
        public async Task Delete__Admin_Can_Delete_Category()
        {
            await LogInAs("admin@gmail.com");
            Category createdCategory = await CreateCategory();
            string deleteEndpoint = ApiRoutes.Category.Delete.Replace("{categoryId}", createdCategory.Id.ToString());
            
            var response = await Client.DeleteAsync(deleteEndpoint);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            int count = await Context.Categories.AsNoTracking().CountAsync();
            count.Should().Be(0);
        }
        
        private async Task<Category> CreateCategory(bool isFeatured = false)
        {
            Category createdCategory = Factory.Category.GetModel(isFeatured);
            await Context.Categories.AddAsync(createdCategory);
            await Context.SaveChangesAsync();
            return createdCategory;
        }
    }
}