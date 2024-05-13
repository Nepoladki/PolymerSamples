using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.Repository;

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
        public async Task<bool> Register(UserWithPasswordDTO user)
        {
            Users newUser = user.FromDTO(_passwordHasher.HashPassword(user.Password));

            if(!await _repository.CreateUserAsync(newUser))
                return false;

            return true;
        }
        public async Task<(bool success, string? token, string? error)>
            Login(string userName, string password)
        {
            if(!await _repository.UserNameExistsAsync(userName))
                return (false, null, "User with this name doesn't exist");

            Users? user = await _repository.GetUserByNameAsync(userName);

            if(_passwordHasher.VerifyHashedPassword(user.HashedPassword, password) == 0)
                return (false, null, "Wrong password");

            string token = _jwtProvider.GenerateToken(user);

            return (true, token, null);
        }
    }
}
