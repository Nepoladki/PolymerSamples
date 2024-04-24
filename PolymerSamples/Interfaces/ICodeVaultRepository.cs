using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeVaultRepository
    {
        ICollection<CodeVault> GetCodeVaults();
        CodeVault GetCodeVault(Guid id);
        bool CodeVaultExists(Guid id);
        bool CreateCodeVault(CodeVault codeVault);
        bool Save();
    }
}
