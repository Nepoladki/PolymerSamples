using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;

namespace PolymerSamples.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository repository, IAuthService authService)
        {
            _authService = authService;
            _repository = repository;
        }

        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegisterNewUserAsync([FromBody] UserWithPasswordDTO user)
        {
            if (user is null)
                return BadRequest(ModelState);

            var existingUser = await _repository.GetUserByNameAsync(user.UserName);

            if (existingUser is not null)
            {
                ModelState.AddModelError("", $"User with name {user.UserName} already exists. Try another name");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user.Password.Length < 6)
            {
                ModelState.AddModelError("", "Password length must be 6 symbols or more");
                return BadRequest(ModelState);
            }

            var result = await _authService.Register(user);

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDto)
        {
            if(loginDto is null)
                return BadRequest(ModelState);

            if(loginDto.login.IsNullOrEmpty() || loginDto.password.IsNullOrEmpty())
                return BadRequest(ModelState);

            var token = await _authService.Login(loginDto.login, loginDto.password);

            Response.Cookies.Append("jwt", token);

            return Ok();
        }
    }
}
