using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
    [Route("api/users/[controller]")]
    [ApiController]
    [Authorize]
    [RequiresClaim(AuthData.RoleClaimType, AuthData.AdminClaimValue)]
    public class UsersController : ControllerBase
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
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

        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] JsonPatchDocument<Users> patchUser)
        {
            var userToUpdate = await _repository.GetUserByIdAsync(id);

            if(userToUpdate is null) 
            {
                ModelState.AddModelError("", $"User with this id {id} doesn't exist");
                return NotFound(ModelState);
            }

            foreach(var op in patchUser.Operations)
            {
                if(op.path == "/Password" && op.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace)
                {
                    if(op.value.ToString()?.Length < 6)
                    {
                        ModelState.AddModelError("", "Password length must be 6 symbols or more");
                        return BadRequest(ModelState);
                    }

                    op.value = _passwordHasher.HashPassword(op.value.ToString());
                    break;
                } 
            }

            patchUser.ApplyTo(userToUpdate, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repository.UpdateUserAsync(userToUpdate))
            {
                ModelState.AddModelError("", $"Error occured while saving updates for user with id {id}");
                return StatusCode(500, ModelState);
            }

            return Ok($"Sucsessfully updated user with id {id}");
                
                
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            if(!await _repository.UserExistsAsync(id))
                return NotFound();

            var user = await _repository.GetUserByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _repository.DeleteUserAsync(user))
            {
                ModelState.AddModelError("",$"Error occured while deleting user with id {id}");
                return BadRequest(ModelState);
            }
                
            return Ok($"Succsesfully deleted user with id {id}");
        }
    }
}
