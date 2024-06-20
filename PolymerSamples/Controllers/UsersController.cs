using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.Services;
using System.Data;

namespace PolymerSamples.Controllers
{
    [Route("api/users/")]
    [ApiController]
    [Authorize(Policy = AuthData.AdminPolicyName)]
    
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(IUserRepository repository, IAuthService authService, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _authService = authService;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<UserDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _repository.GetAllUsersAsync();

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(users.Select(u => u.AsDTO()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            if(!await _repository.UserExistsAsync(id))
                return NotFound();
                
            var user = await _repository.GetUserByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user.AsDTO());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserWithPasswordDTO userDto)
        {
            if (userDto is null)
                return BadRequest("Invalid user object");

            var user = await _repository.GetUserByIdAsync(id);

            if (!userDto.password.IsNullOrEmpty() && userDto.password.Length > 5)
                user.HashedPassword = _passwordHasher.HashPassword(userDto.password);

            user.UserName = userDto.username;
            user.Role = userDto.role;
            user.IsActive = userDto.is_active;

            if (!await _repository.UpdateUserAsync(user))
                return StatusCode(500, $"Error occured while saving updated user with id {id}");

            return Ok("Successfully updated");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegisterNewUserAsync([FromBody] UserWithPasswordDTO user)
        {
            if (user is null)
                return BadRequest("Invalid user object");

            var existingUser = await _repository.GetUserByNameAsync(user.username);

            if (existingUser is not null)
                return BadRequest($"User with name {user.username} already exists. Try another name");

            if (user.password.Length < 6)
                return BadRequest("Password length must be 6 symbols or more");

            if (!await _authService.RegisterAsync(user))
                return StatusCode(500, "Registration failure");

            return Ok("Registration completed!");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            if(!await _repository.UserExistsAsync(id))
                return NotFound("User does not exist");

            var user = await _repository.GetUserByIdAsync(id);

            if (!await _repository.DeleteUserAsync(user))
                return BadRequest($"Error occured while deleting user with id {id}");
                
            return Ok($"Succsesfully deleted user with id {id}");
        }
    }
}
