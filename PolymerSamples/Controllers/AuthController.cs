using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PolymerSamples.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;
        private readonly JwtSecurityTokenHandler _jwtHandler;

        public AuthController(IUserRepository repository, IAuthService authService, JwtSecurityTokenHandler jwtHandler)
        {
            _authService = authService;
            _repository = repository;
            _jwtHandler = jwtHandler;
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

            Response.Cookies.Append(AuthData.RefreshTokenName, result.Value.RefreshToken);

            return Ok($"Welcome, {loginDto.login}, your access token: {result.Value.JwtToken}");
        }

        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RefreshAsync()
        {   // Checking if user have refresh token in cookies
            if (!Request.Cookies.TryGetValue(AuthData.RefreshTokenName, out string? userRefreshToken))
                return Unauthorized("Did not found your refresh token in cookies");

            string jwtToken = HttpContext.Request.Headers.Authorization.ToString().Split()[1];
            var decodedToken = _jwtHandler.ReadJwtToken(jwtToken);
          
            // Checking if user's jwt token isn't expired
            DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(
                long.Parse(decodedToken.Claims.First(c => c.Type == "exp").Value)).UtcDateTime;
            if (utcDateTime > DateTime.UtcNow)
                return Ok(jwtToken);

            var refreshResult = await _authService.RefreshAsync(decodedToken.Claims, userRefreshToken);

            if (refreshResult.IsFailure)
                return StatusCode(500, refreshResult.Error);
            
            Response.Cookies.Append(AuthData.RefreshTokenName, refreshResult.Value.RefreshToken);

            return Ok($"Successfully refreshed, your new access token: {refreshResult.Value.JwtToken}");
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
