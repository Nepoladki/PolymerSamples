using PolymerSamples.DTO;
using PolymerSamples.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PolymerSamples.Authorization
{
    public interface IJwtProvider
    {
        JwtAuthDataDTO GenerateToken(Users user);
    }
}
