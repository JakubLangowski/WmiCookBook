using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WmiCookBook.Contracts;
using WmiCookBook.Contracts.Request;
using WmiCookBook.Contracts.Request.Queries;
using WmiCookBook.Contracts.Request.Recipe;
using WmiCookBook.Contracts.Response;
using WmiCookBook.Contracts.Response.Errors;
using WmiCookBook.Contracts.Response.Recipe;
using WmiCookBook.Helpers;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;
using WmiCookBook.Services.Interfaces;

namespace WmiCookBook.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RecipeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService, IUriService uriService, IMapper mapper)
        {
            _recipeService = recipeService;
            _uriService = uriService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Zwraca listę wszystkich przepisów (stronnicowanie)
        /// </summary>
        /// <param name="paginationQuery"></param>
        /// <param name="recipeQuery"></param>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(PagedResponse<List<RecipeResponse>>))]
        //
        [HttpGet(ApiRoutes.Recipe.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] RecipeQuery recipeQuery)
        {
            paginationQuery = PaginationHelper.ValidateQuery(paginationQuery);
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var recipeFilter = _mapper.Map<RecipeFilter>(recipeQuery);
            
            var recipeList = await _recipeService.GetAllRecipeAsync(paginationFilter, recipeFilter);
            var recipeCount = await _recipeService.RecipeCountAsync(recipeFilter);
            
            var recipeResponse = _mapper.Map<List<RecipeResponse>>(recipeList);
            var paginatedResponse =
                PaginationHelper.Paginate(_uriService, paginationFilter, recipeResponse, recipeCount);
            
            return Ok(paginatedResponse);
        }
        
        /// <summary>
        /// Zwraca listę wyróżnionych przepisów (max 4 przepisy)
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(List<RecipeResponse>))]
        //
        [HttpGet(ApiRoutes.Recipe.GetFeatured)]
        public async Task<IActionResult> GetFeaturedRecipes()
        {
            var recipeList = await _recipeService.GetFeaturedRecipeAsync();
            var recipeResponse = _mapper.Map<List<RecipeResponse>>(recipeList);
            return Ok(recipeResponse);
        }
        
        /// <summary>
        /// Zwraca listę nowych przepisów (max 8 przepisów)
        /// </summary>
        /// <response code="200"></response>
        [SwaggerResponse(200, "", typeof(List<RecipeResponse>))]
        //
        [HttpGet(ApiRoutes.Recipe.GetNew)]
        public async Task<IActionResult> GetNewRecipes()
        {
            var recipeList = await _recipeService.GetNewRecipeAsync();
            var recipeResponse = _mapper.Map<List<RecipeResponse>>(recipeList);
            return Ok(recipeResponse);
        }
        
        /// <summary>
        /// Zwraca pojedyńczy przepis ze składnikami i krokami
        /// </summary>
        /// <param name="recipeId"></param>
        /// <response code="200"></response>
        /// <response code="404"></response>
        [SwaggerResponse(200, "", typeof(RecipeResponse))]
        [SwaggerResponse(404)]
        //
        [HttpGet(ApiRoutes.Recipe.Get, Name = "GetRecipeById")]
        public async Task<IActionResult> GetRecipeById([FromRoute] int recipeId)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);
            if (recipe == null)
                return NotFound();

            return Ok(_mapper.Map<RecipeFullResponse>(recipe));
        }
        
        
        /// <summary>
        /// Tworzy nowy przepis
        /// </summary>
        /// <param name="request"></param>
        /// <response code="201"></response>
        /// <response code="400"></response>
        [SwaggerResponse(201, "", typeof(RecipeResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        
        [Consumes("multipart/form-data")]
        [HttpPost(ApiRoutes.Recipe.Create)]
        public async Task<IActionResult> Create([FromForm] CreateRecipeRequest request)
        {
            var newRecipe = _mapper.Map<Recipe>(request);
            var createdRecipe = await _recipeService.CreateRecipeAsync(newRecipe, request.Image);
            
            if (createdRecipe.Id == 0)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawania"));
            
            return CreatedAtAction(nameof(GetRecipeById), 
                new {recipeId = createdRecipe.Id},
                _mapper.Map<RecipeFullResponse>(createdRecipe));
        }

        /// <summary>
        /// Dodaje lub usuwa przepis z wyróżnionych
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="featuredRequest"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize]
        [HttpPatch(ApiRoutes.Recipe.AddToFeatured)]
        public async Task<IActionResult> AddToFeatured([FromRoute] int recipeId, [FromBody] FeaturedRequest featuredRequest)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);
            
            if (recipe == null)
                return NotFound();

            var updated = await _recipeService.AddRecipeToFeaturedAsync(recipe, featuredRequest);
            
            if (!updated)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas dodawanie przepisu do wyróżnionych"));

            return Ok();
        }
        
        /// <summary>
        /// Ustawia przepis jako zaakceptowany przez admina
        /// </summary>
        /// <param name="recipeId"></param>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize]
        [HttpPatch(ApiRoutes.Recipe.AcceptRecipe)]
        public async Task<IActionResult> AcceptRecipe([FromRoute] int recipeId)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);
            
            if (recipe == null)
                return NotFound();

            var updated = await _recipeService.AcceptRecipeAsync(recipe);
            
            if (!updated)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas akceptowania przepisu"));

            return Ok();
        }
        
        /// <summary>
        /// Usuwa przepis
        /// </summary>
        /// <param name="recipeId"></param>
        /// <response code="204"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [SwaggerResponse(204)]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        [SwaggerResponse(404)]
        //
        [Authorize]
        [HttpDelete(ApiRoutes.Recipe.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int recipeId)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeId);
            
            if (recipe == null)
                return NotFound();

            var deleted = await _recipeService.DeleteRecipeAsync(recipe);
            
            if (!deleted)
                return BadRequest(new ErrorResponse("Wystąpił błąd podczas usuwania"));

            return NoContent();
        }
    }
}
