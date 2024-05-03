using PolymerSamples.DTO;
using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeRepository
    {
        ICollection<CodesIncludesVaultsDTO> GetCodes();
        Codes GetCode(Guid id);
        bool CodeExists(Guid id);
        bool CreateCode(Codes code);
        bool UpdateCode(Codes code);
        bool DeleteCode(Codes code);
        bool Save();

    }
}
