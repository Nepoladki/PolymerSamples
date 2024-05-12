using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(UserWithPasswordDTO user);
        Task<string> Login(string userName, string password);
        
    }
}
