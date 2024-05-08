using PolymerSamples.Models;
using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces
{
    public interface IVaultRepository
    {
        Task<ICollection<VaultIncludesCodesDTO>> GetAllVaultsAsync();
        Task<VaultIncludesCodesDTO> GetVaultWithCodesByIdAsync(Guid id);
        Task<ICollection<VaultIncludesCodesDTO>?> GetVaultWithCodesAndCivIdAsync(Guid vaultId);
        Task<Vaults> GetVaultByIdAsync(Guid id);
        Task<Vaults?> GetVaultByNameAsync(string name);
        Task<bool> VaultExistsAsync(Guid id);
        Task<bool> CreateVaultAsync(Vaults vault);
        Task<bool> DeleteVaultAsync(Vaults vault);
        Task<bool> UpdateVaultAsync(Vaults vault);
        Task<bool> SaveAsync();
    }
}
