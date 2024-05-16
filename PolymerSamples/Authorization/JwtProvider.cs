using PolymerSamples.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Claims;
using PolymerSamples.DTO;
using System.Security.Cryptography;

namespace PolymerSamples.Authorization
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public JwtAuthDataDTO GenerateToken(Users user)
        { 
            Claim[] claims = 
            [
                new Claim("userId", user.Id.ToString()),
                new Claim("role", user.Role.ToString())
            ];

            var expiringTime = DateTime.UtcNow.AddMinutes(_options.ExpiresMinutes);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), 
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: expiringTime);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            var authData = new JwtAuthDataDTO()
            {
                JwtToken = tokenValue,
                Expiration = expiringTime,
                RefreshToken = null
            };

            return authData;
        }
        public (string token, DateTime expires) GenerateRefreshToken()
        {
            var number = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(number);

            return (Convert.ToBase64String(number), 
                DateTime.UtcNow.AddHours(_options.RefreshExpiresHours));
        }
        public bool ValidateToken(string token)
        {
            //var secret = _options.SecretKey;

            //var validation = new TokenValidationParameters
            //{
            //    ClockSkew = new TimeSpan(0, 0, 5),
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            //    ValidateLifetime = true,
            //    ValidateAudience = false,
            //    ValidateIssuer = false,
            //    ValidateIssuerSigningKey = true,
            //};
            return false;
            //return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }
    }
}
