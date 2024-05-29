using CSharpFunctionalExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using NuGet.Versioning;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PolymerSamples.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository repository, 
            IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> RegisterAsync(UserWithPasswordDTO user)
        {
            Users newUser = user.FromDTO(_passwordHasher.HashPassword(user.password));

            if (!await _repository.CreateUserAsync(newUser))
                return false;

            return true;
        }
        public async Task<Result<JwtAuthDataDTO>> LoginAsync(string userName, string password)
        {
            Users? user = await _repository.GetUserByNameAsync(userName);

            if (user is null)
                return Result.Failure<JwtAuthDataDTO>($"User with name {userName} name doesn't exist");

            if (!user.IsActive)
                return Result.Failure<JwtAuthDataDTO>($"User with name {userName} is marked as inactive");

            if (_passwordHasher.VerifyHashedPassword(user.HashedPassword, password) == 0)
                return Result.Failure<JwtAuthDataDTO>($"Wrong password");

            JwtAuthDataDTO authData = _jwtProvider.GenerateToken(user);

            user.RefreshToken = authData.RefreshToken;
            user.RefreshExpires = authData.RefreshExpires;

            await _repository.UpdateUserAsync(user);

            return Result.Success(authData);
        }
        public async Task<Result<JwtAuthDataDTO>> RefreshAsync(Users user, string refreshToken)
        {
            if (user is null || !user.IsActive || user.RefreshToken != refreshToken || user.RefreshExpires < DateTime.UtcNow)
                return Result.Failure<JwtAuthDataDTO>("User is inactive or refresh token expired, try to log in manually");

            var newAuthData = _jwtProvider.GenerateToken(user);

            user.RefreshToken = newAuthData.RefreshToken;
            user.RefreshExpires = newAuthData.RefreshExpires;

            if (!await _repository.UpdateUserAsync(user))
                return Result.Failure<JwtAuthDataDTO>("Error occured while saving new refresh token in database");

            return Result.Success(newAuthData);
        }
    }
}
