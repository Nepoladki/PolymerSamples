using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeVaultRepository
    {
        Task<ICollection<CodesVaults>> GetAllCodeVaultsAsync();
        Task<CodesVaults> GetCodeVaultByIdAsync(Guid id);
        Task<bool> CodeVaultExistsAsync(Guid id);
        Task<bool> CreateCodeVaultAsync(CodesVaults codeVault);
        Task<bool> DeleteCodeVaultAsync(CodesVaults codeVault);
        Task<bool> SaveAsync();
        Task<bool> UpdateCodeVaultAsync(CodesVaults codesVaults);
    }
}
