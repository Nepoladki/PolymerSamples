using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(UserWithPasswordDTO user);
        Task<(bool success, JwtAuthDataDTO? authData, string? error)> LoginAsync(string userName, string password);
        Task<(bool, JwtAuthDataDTO?)> RefreshAsync(string jwtToken, string refreshToken);
    }
}
