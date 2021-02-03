﻿using WmiCookBook.Contracts.Request.Auth;
using WmiCookBook.Models;
using System.Threading.Tasks;

namespace WmiCookBook.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterAsync(AuthRegisterRequest request);
        Task<AuthenticationResult> LoginAsync(AuthLoginRequest request);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<AuthenticationResult> ChangePasswordAsync(AuthChangePasswordRequest request);
    }
}