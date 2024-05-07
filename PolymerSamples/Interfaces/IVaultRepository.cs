using PolymerSamples.Models;
using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces
{
    public interface IVaultRepository
    {
        ICollection<VaultIncludesCodesDTO> GetVaults();
        VaultIncludesCodesDTO GetVaultWithCodes(Guid id);
        VaultWithAllIncludingDataDTO GetVaultWithAllIncludingData(Guid id);
        Vaults GetVault(Guid id);
        bool VaultExists(Guid id);
        bool CreateVault(Vaults vault);
        bool DeleteVault(Vaults vault);
        bool UpdateVault(Vaults vault);
        bool Save();
    }
}
