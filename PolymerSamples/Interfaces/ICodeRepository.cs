using PolymerSamples.DTO;
using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeRepository
    {
        Task<ICollection<CodeIncludesVaultsDTO>> GetAllCodesIncludingVaultsAsync();
        Task<Codes?> GetCodeByNameAsync(string name);
        Task<Codes> GetCodeByIdAsync(Guid id);
        Task<bool> CodeExistsAsync(Guid id);
        Task<bool> CreateCodeAsync(Codes code);
        Task<bool> UpdateCodeAsync(Codes code);
        Task<bool> DeleteCodeAsync(Codes code);
        Task<bool> SaveAsync();

    }
}
