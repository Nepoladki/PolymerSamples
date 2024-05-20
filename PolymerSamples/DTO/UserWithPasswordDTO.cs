using PolymerSamples.Authorization;

namespace PolymerSamples.DTO
{
    public record class UserWithPasswordDTO
    (
      Guid Id,
      string UserName,
      string Password,
      string Role,
      bool IsActive,
      string RefreshToken,
      DateTime RefreshExpires
    );
}
