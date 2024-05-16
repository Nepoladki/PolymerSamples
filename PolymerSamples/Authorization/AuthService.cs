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
            Users newUser = user.FromDTO(_passwordHasher.HashPassword(user.Password));

            if (!await _repository.CreateUserAsync(newUser))
                return false;

            return true;
        }
        public async Task<(bool success, JwtAuthDataDTO? authData, string? error)>
            LoginAsync(string userName, string password)
        {
            Users? user = await _repository.GetUserByNameAsync(userName);

            if (user is null)
                return (false, null, "User with this name doesn't exist");

            if (_passwordHasher.VerifyHashedPassword(user.HashedPassword, password) == 0)
                return (false, null, "Wrong password");

            JwtAuthDataDTO authData = _jwtProvider.GenerateToken(user);

            var refreshToken = _jwtProvider.GenerateRefreshToken();

            authData.RefreshToken = refreshToken.token;
            user.RefreshToken = refreshToken.token;
            user.RefreshExpires = refreshToken.expires;

            await _repository.UpdateUserAsync(user);

            return (true, authData, null);
        }
        public async Task<(bool, JwtAuthDataDTO?)> RefreshAsync(string jwtToken, string refreshToken)
        {
            var encodedToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
            
            string userId = encodedToken.Claims.FirstOrDefault(k => k.Type == "userId").Value;
            
            if (userId.IsNullOrEmpty())
                return (false, null);

            var user = await _repository.GetUserByIdAsync(Guid.Parse(userId));

            if (user is null || !user.IsActive || user.RefreshToken != refreshToken || user.RefreshExpires < DateTime.UtcNow)
                return (false, null);

            string newRefreshToken;
            DateTime newRefreshExpires;

            var newAuthData = _jwtProvider.GenerateToken(user);

            (newRefreshToken, newRefreshExpires) = _jwtProvider.GenerateRefreshToken();

            newAuthData.RefreshToken = newRefreshToken;

            user.RefreshToken = newRefreshToken;
            user.RefreshExpires = newRefreshExpires;

            if (!await _repository.UpdateUserAsync(user))
                return (false, null);

            return (true, newAuthData);
        }
    }
}
