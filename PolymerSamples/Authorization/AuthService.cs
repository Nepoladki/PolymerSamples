using Microsoft.AspNet.Identity;
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

        public AuthService(IUserRepository repository, IJwtProvider jwtProvider)
        {
            _repository = repository;
            _jwtProvider = jwtProvider;
        }
        public async Task<string> Register(UserWithPasswordDTO user)
        {
            var hasher = new PasswordHasher();

            Users newUser = user.FromDTO(hasher.HashPassword(user.Password));

            if(!await _repository.CreateUserAsync(newUser))
                return "Error occured while saving new user";

            return "Registration completed";
        }
        public async Task<string> Login(string userName, string password)
        {
            if (!await _repository.UserNameExistsAsync(userName))
                return "User with this name doesn't exist";

            Users? user = await _repository.GetUserByNameAsync(userName);
            
            var hasher = new PasswordHasher();

            if (hasher.VerifyHashedPassword(user.HashedPassword, password) == 0)
                return "Wrong password";

            string token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
