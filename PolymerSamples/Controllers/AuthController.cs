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

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDto)
        {
            if (loginDto is null)
                return BadRequest("Invalid login object");

            if (loginDto.login.IsNullOrEmpty() || loginDto.password.IsNullOrEmpty())
                return BadRequest("Login or password is null or empty");

            var result = await _authService.LoginAsync(loginDto.login, loginDto.password);

            if (result.IsFailure)
                return BadRequest(result.Error);

            Response.Cookies.Append(AuthData.RefreshTokenName, result.Value.RefreshToken);

            return Ok(new { accessToken = result.Value.JwtToken });
        }

        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RefreshAsync()
        {
            //DEBUG ONLY
            Console.WriteLine("v--------------Request reached refresh endpoint-------------v");
            Console.WriteLine("Headers:");
            foreach (var item in HttpContext.Request.Headers)
            {
                Console.WriteLine($"Header {item.Key} contains {item.Value}");
            }
            Console.WriteLine("^--------------End of debug section------------^");
            
            // Checking if user have refresh token in cookies
            if (!Request.Cookies.TryGetValue(AuthData.RefreshTokenName, out string? userRefreshToken))
                return Unauthorized("Did not found your refresh token in cookies");

            var user = await _repository.GetUserByRefreshTokenAsync(userRefreshToken);

            if (user is null)
                return StatusCode(401, "Invalid refresh token");

            var refreshResult = await _authService.RefreshAsync(user, userRefreshToken);

            if (refreshResult.IsFailure)
                return StatusCode(500, refreshResult.Error);

            Response.Cookies.Append(AuthData.RefreshTokenName, refreshResult.Value.RefreshToken);

            return Ok(new { accessToken = refreshResult.Value.JwtToken });
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
