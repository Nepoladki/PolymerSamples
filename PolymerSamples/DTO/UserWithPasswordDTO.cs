namespace PolymerSamples.DTO
{
    public record class UserWithPasswordDTO
    (
      Guid Id,
      string UserName,
      string Password,
      List<string> Roles,
      bool IsActive
    );
}
