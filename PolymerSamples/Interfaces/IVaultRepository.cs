using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface IVaultRepository
    {
        ICollection<Vault> GetVaults();
        Vault GetVault(Guid id);
        bool VaultExists(Guid id);
        bool CreateVault(Vault vault);
        bool DeleteVault(Vault vault);
        bool UpdateVault(Vault vault);
        bool Save();
    }
}
