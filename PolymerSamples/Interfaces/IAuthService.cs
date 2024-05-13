using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(UserWithPasswordDTO user);
        Task<(bool success, string? token, string? error)> Login(string userName, string password);
        
    }
}
