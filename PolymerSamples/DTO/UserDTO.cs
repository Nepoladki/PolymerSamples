using PolymerSamples.Authorization;

namespace PolymerSamples.DTO
{
    public record class UserDTO
    (
        Guid Id,
        string UserName,
        string Role,
        bool IsActive
    );
}
