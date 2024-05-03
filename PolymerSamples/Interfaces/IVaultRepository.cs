using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface IVaultRepository
    {
        ICollection<Vaults> GetVaults();
        Vaults GetVault(Guid id);
        bool VaultExists(Guid id);
        bool CreateVault(Vaults vault);
        bool DeleteVault(Vaults vault);
        bool UpdateVault(Vaults vault);
        bool Save();
    }
}
