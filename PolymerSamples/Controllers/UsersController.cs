using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;

namespace PolymerSamples.Controllers
{
    [Route("api/users/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
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
        [ProducesResponseType(200, Type = typeof(Users))]
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

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostUsersAsync([FromBody] UserWithPasswordDTO user)
        {
            if(user is null)
                return BadRequest(ModelState);

            var existingUser = await _repository.GetUserByNameAsync(user.UserName);

            if(existingUser is not null)
            {
                ModelState.AddModelError("", $"User with name {user.UserName} already exists. Try another name");
                return BadRequest(ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(user.Password.Length < 6)
            {
                ModelState.AddModelError("", "Password length must be 6 symbols or more");
                return BadRequest(ModelState);
            }

            var hasher = new PasswordHasher();
            string hashedPassword = hasher.HashPassword(user.Password);

            var newUser = user.FromDTO(hashedPassword);

            if (!await _repository.CreateUserAsync(newUser))
            {
                ModelState.AddModelError("", $"Error occured while saving new user {user.UserName}");
                return StatusCode(500, ModelState);
            }

            return Ok(newUser.AsDTO());
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
                    if(op.value.ToString().Length < 6)
                    {
                        ModelState.AddModelError("", "Password length must be 6 symbols or more");
                        return BadRequest(ModelState);
                    }

                    var hasher = new PasswordHasher();
                    op.value = hasher.HashPassword(op.value.ToString());
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
