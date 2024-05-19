using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(UserWithPasswordDTO user);
        Task<Result<JwtAuthDataDTO>> LoginAsync(string userName, string password);
        Task<Result<JwtAuthDataDTO>> RefreshAsync(string jwtToken, string refreshToken);
    }
}
