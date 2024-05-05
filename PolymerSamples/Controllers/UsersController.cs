using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolymerSamples.Data;
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
        [ProducesResponseType(400)]
        public IActionResult GetUsers(Guid id)
        {
            if (_repository.UserExists(id))
                return NotFound();
                
            var user = _repository.GetUser(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult PostUsers([FromBody]CreateUserDTO user)
        {
            if(user is null)
                return BadRequest(ModelState);

            var existingUser = _repository.GetAllUsers().Where(u => u.UserName.Trim() == user.UserName.Trim());

            if(existingUser is null)
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

            var newUser = user.FromDTO();

            if (!_repository.CreateUser(newUser))
            {
                ModelState.AddModelError("", $"Error occure while saving new user {user.UserName}");
                return StatusCode(500, ModelState);
            }

            return Ok(newUser); //Возвращать надо DTO без пароля, а не то что возвращается сейчас
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
