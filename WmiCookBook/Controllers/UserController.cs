using AutoMapper;
using WmiCookBook.Contracts;
using WmiCookBook.Contracts.Request.Queries;
using WmiCookBook.Contracts.Response;
using WmiCookBook.Contracts.Response.Errors;
using WmiCookBook.Contracts.Response.User;
using WmiCookBook.Data;
using WmiCookBook.Helpers;
using WmiCookBook.Models;
using WmiCookBook.Models.Filters;
using WmiCookBook.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WmiCookBook.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IAuthHelper _authHelper;
        private readonly DatabaseContext _context;

        public UserController(IUserService userService, IMapper mapper, IUriService uriService, IAuthHelper authHelper,
            DatabaseContext context /**/)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
            _authHelper = authHelper;
            _context = context;
        }

        /// <summary>
        /// Zwraca listę wszystkich użytkowników
        /// </summary>
        /// <param name="paginationQuery"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(PagedResponse<List<UserResponse>>))]
        //
        [HttpGet(ApiRoutes.User.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery)
        {
            paginationQuery = PaginationHelper.ValidateQuery(paginationQuery);
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);

            var users = await _userService.GetAllUsersAsync(paginationFilter);
            var usersCount = await _userService.CountUsersAsync();

            var userResponses = _mapper.Map<List<UserResponse>>(users);
            var paginatedResponse =
                PaginationHelper.Paginate(_uriService, paginationFilter, userResponses, usersCount);

            return Ok(paginatedResponse);
        }

        /// <summary>
        /// Zwraca zalogowanego użytkownika
        /// </summary>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(UserResponse))]
        //
        [HttpGet(ApiRoutes.User.Get)]
        public async Task<IActionResult> GetById()
        {
            var user = await _userService.GetUserByIdAsync(_authHelper.GetAuthenticatedUserId());
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserResponse>(user));
        }
    }
}