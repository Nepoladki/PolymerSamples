namespace PolymerSamples.DTO
{
    public record class UserDTO
    (
        Guid Id,
        string UserName,
        List<string> Roles,
        bool IsActive
    );
}
