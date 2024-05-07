using PolymerSamples.DTO;
using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeRepository
    {
        ICollection<CodeIncludesVaultsDTO> GetAllCodesIncludingVaults();
        Codes? GetCodeWithCurrentName(string name);
        Codes GetCodeById(Guid id);
        bool CodeExists(Guid id);
        bool CreateCode(Codes code);
        bool UpdateCode(Codes code);
        bool DeleteCode(Codes code);
        bool Save();

    }
}
