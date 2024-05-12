using PolymerSamples.Models;

namespace PolymerSamples.Authorization
{
    public interface IJwtProvider
    {
        string GenerateToken(Users user);
    }
}
