using PolymerSamples.Models;
using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces
{
    public interface IVaultRepository
    {
        ICollection<VaultIncludesCodesDTO> GetAllVaults();
        VaultIncludesCodesDTO GetVaultWithCodes(Guid id);
        ICollection<VaultIncludesCodesDTO>? GetVaultWithCodesAndCivId(Guid vaultId);
        Vaults GetVault(Guid id);
        bool VaultExists(Guid id);
        bool CreateVault(Vaults vault);
        bool DeleteVault(Vaults vault);
        bool UpdateVault(Vaults vault);
        bool Save();
    }
}
