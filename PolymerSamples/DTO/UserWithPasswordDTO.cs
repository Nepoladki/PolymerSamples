using PolymerSamples.Authorization;

namespace PolymerSamples.DTO
{
    public record class UserWithPasswordDTO
    (
      Guid id,
      string username,
      string password,
      string role,
      bool is_active,
      string RefreshToken,
      DateTime RefreshExpires
    );
}
