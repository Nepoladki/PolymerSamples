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
        [ProducesResponseType(200, Type = typeof(ICollection<Users>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllUsers()
        {
            var users = _repository.GetAllUsers();

            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            return Ok(users);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Users))]
        [ProducesResponseType(404)]
        public IActionResult GetUser(Guid id)
        {
            if(!_repository.UserExists(id))
                return NotFound();
                
            var user = _repository.GetUser(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user.AsDTO());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult PostUsers([FromBody] UserWithPasswordDTO user)
        {
            if(user is null)
                return BadRequest(ModelState);

            var existingUser = _repository.GetAllUsers().Where(u => u.UserName.Trim() == user.UserName.Trim());

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

            if (!_repository.CreateUser(newUser))
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
        public IActionResult UpdateUser(Guid id, [FromBody] JsonPatchDocument<Users> patchUser)
        {
            var userToUpdate = _repository.GetUser(id);

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

            if (!_repository.UpdateUser(userToUpdate))
            {
                ModelState.AddModelError("", $"Error occured while saving updates for user with id {id}");
                return StatusCode(500, ModelState);
            }

            return Ok($"Sucsessfully updated user with id {id}");
                
                
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        public IActionResult DeleteUser(Guid id)
        {
            if(!_repository.UserExists(id))
                return NotFound();

            var user = _repository.GetUser(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.DeleteUser(user))
            {
                ModelState.AddModelError("",$"Error occured while deleting user with id {id}");
                return BadRequest(ModelState);
            }
                
            return Ok($"Succsesfully deleted user with id {id}");
        }
    }
}
