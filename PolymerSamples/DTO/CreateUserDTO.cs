namespace PolymerSamples.DTO
{
    public record class CreateUserDTO
    (
      Guid Id,
      string UserName,
      string Password,
      List<string> Roles,
      bool IsActive
    );
}
