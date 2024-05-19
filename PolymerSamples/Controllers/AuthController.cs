using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using System;

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
        [ProducesResponseType(400)]
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

            if (!await _authService.RegisterAsync(user))
                return StatusCode(500, "Registration failure");

            return Ok("Registration completed!");
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDto)
        {
            if (loginDto is null)
                return BadRequest(ModelState);

            if (loginDto.login.IsNullOrEmpty() || loginDto.password.IsNullOrEmpty())
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(loginDto.login, loginDto.password);

            if (result.IsFailure)
                return BadRequest(result.Error);

            Response.Cookies.Append(AuthData.AccessTokenName, result.Value.JwtToken);
            Response.Cookies.Append(AuthData.RefreshTokenName, result.Value.RefreshToken);

            return Ok($"Welcome, {loginDto.login}!");
        }

        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RefreshAsync()
        {
            if (!Request.Cookies.TryGetValue(AuthData.RefreshTokenName, out string userRefreshToken))
                return Unauthorized("Did not found your refresh token in cookies");

            if (!Request.Cookies.TryGetValue(AuthData.AccessTokenName, out string userJwtToken)) //По идее запрос попадает в этот раут только если у юзера есть истекший токен, не знаю, нужна ли эта валидация на самом деле
                return Unauthorized("Did not found your jwt token in cookies");

            var refreshResult = await _authService.RefreshAsync(userJwtToken, userRefreshToken);

            if (refreshResult.IsFailure)
                return Unauthorized(refreshResult.Error);
            
            Response.Cookies.Append(AuthData.AccessTokenName, refreshResult.Value.JwtToken);
            Response.Cookies.Append(AuthData.RefreshTokenName, refreshResult.Value.RefreshToken);

            return Ok(refreshResult.Value.JwtToken);
        }

        [Authorize]
        [HttpDelete("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(AuthData.AccessTokenName);
            Response.Cookies.Delete(AuthData.RefreshTokenName);

            return Ok();
        }
    }
}
