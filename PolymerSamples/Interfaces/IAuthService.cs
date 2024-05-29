using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PolymerSamples.DTO;
using PolymerSamples.Models;
using System.Security.Claims;

namespace PolymerSamples.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(UserWithPasswordDTO user);
        Task<Result<JwtAuthDataDTO>> LoginAsync(string userName, string password);
        Task<Result<JwtAuthDataDTO>> RefreshAsync(Users user, string refreshToken);
    }
}
