using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeVaultRepository
    {
        ICollection<CodesVaults> GetCodeVaults();
        CodesVaults GetCodeVault(Guid id);
        bool CodeVaultExists(Guid id);
        bool CreateCodeVault(CodesVaults codeVault);
        bool DeleteCodeVault(CodesVaults codeVault);
        bool Save();
    }
}
