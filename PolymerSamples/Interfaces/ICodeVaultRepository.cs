using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeVaultRepository
    {
        ICollection<CodesVaults> GetAllCodeVaults();
        CodesVaults GetCodeVaultById(Guid id);
        bool CodeVaultExists(Guid id);
        bool CreateCodeVault(CodesVaults codeVault);
        bool DeleteCodeVault(CodesVaults codeVault);
        bool Save();
    }
}
