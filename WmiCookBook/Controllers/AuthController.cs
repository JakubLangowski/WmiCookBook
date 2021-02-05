﻿using WmiCookBook.Contracts;
using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Contracts.Response.Auth;
using WmiCookBook.Contracts.Response.Errors;
using WmiCookBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WmiCookBook.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Rejestruje nowego użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(AuthSuccessResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
        {
            var authResponse = await _authService.RegisterAsync(request);

            if (!authResponse.Success)
                return BadRequest(new ErrorResponse(authResponse.Error));

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Loguje istniejącego użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(AuthSuccessResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest request)
        {
            var authResponse = await _authService.LoginAsync(request);

            if (!authResponse.Success)
                return BadRequest(new ErrorResponse(authResponse.Error));

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Generuje nowy token JWT
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(AuthSuccessResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> RefreshToken([FromBody] AuthRefreshTokenRequest request)
        {
            var authResponse = await _authService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
                return BadRequest(new ErrorResponse(authResponse.Error));

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        /// <summary>
        /// Zmienia hasło istniejącego użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200"></response>
        ///  <response code="400"></response>
        [SwaggerResponse(200, "", typeof(AuthSuccessResponse))]
        [SwaggerResponse(400, "", typeof(ErrorResponse))]
        //
        [HttpPut(ApiRoutes.Auth.ChangePassword)]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] AuthChangePasswordRequest request)
        {
            var authResponse = await _authService.ChangePasswordAsync(request);

            if (!authResponse.Success)
                return BadRequest(new ErrorResponse(authResponse.Error));

            return Ok();
        }
    }
}