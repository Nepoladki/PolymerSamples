using PolymerSamples.DTO;
using PolymerSamples.Models;
using System.Security.Claims;

namespace PolymerSamples.Authorization
{
    public interface IJwtProvider
    {
        JwtAuthDataDTO GenerateToken(Users user);
        (string token, DateTime expires) GenerateRefreshToken();
        ClaimsPrincipal GetTokenPrincipal(string token);
    }
}
