using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WmiCookBook.Contracts;
using WmiCookBook.Contracts.Request;
using WmiCookBook.Contracts.Request.Category;
using WmiCookBook.Contracts.Response;
using WmiCookBook.Contracts.Response.Category;
using WmiCookBook.Contracts.Response.Errors;
using WmiCookBook.Models;
using WmiCookBook.Services.Interfaces;

namespace WmiCookBook.Controllers
{
    
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Zwraca listę wszystkich kategorii
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(PagedResponse<List<CategoryResponse>>))]
        //
        [HttpGet(ApiRoutes.Category.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync());
        }
        
        /// <summary>
        /// Zwraca listę wyróżnionych kategorii (max 4 kategorie)
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(List<CategoryResponse>))]
        //
        [HttpGet(ApiRoutes.Category.GetFeatured)]
        public async Task<IActionResult> GetFeaturedCategories()
        {
            return Ok(await _categoryService.GetFeaturedCategoriesAsync());
        }
        
        /// <summary>
        /// Zwraca pojedyńczą kategorię
        /// </summary>
        /// <param name="categoryId"></param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200, "", typeof(CategoryResponse))]
        [SwaggerResponse(404)]
        //
        [HttpGet(ApiRoutes.Category.Get, Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryResponse>(category));
        }

        /// <summary>
        /// Tworzy nową kategorię
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201"></response>
        /// <response code="400"></response>
        [SwaggerResponse(201, "", typeof(CategoryResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Category.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var newCategory = _mapper.Map<Category>(request);
            var createdCategory = await _categoryService.CreateCategoryAsync(newCategory);
            
            if (createdCategory.Id == 0)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania"));
            
            return CreatedAtAction(nameof(GetCategoryById), 
                new {categoryId = createdCategory.Id},
                _mapper.Map<CategoryResponse>(createdCategory));
        }
        
        /// <summary>
        /// Dodaje lub usuwa kategorię z wyróżnionych
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="featuredRequest"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch(ApiRoutes.Category.AddToFeatured)]
        public async Task<IActionResult> AddToFeatured([FromRoute] int categoryId, [FromBody] FeaturedRequest featuredRequest)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            
            if (category == null)
                return NotFound();

            var updated = await _categoryService.AddCategoryToFeaturedAsync(category, featuredRequest);
            
            if (!updated)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawanie kategorii do wyróżnionych"));

            return Ok();
        }
        
        /// <summary>
        /// Usuwa kategorie
        /// </summary>
        /// <param name="categoryId"></param>
        /// <response code="204"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(ApiRoutes.Category.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            
            if (category == null)
                return NotFound();

            var deleted = await _categoryService.DeleteCategoryAsync(category);
            
            if (!deleted)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas usuwania"));

            return NoContent();
        }
    }
}
