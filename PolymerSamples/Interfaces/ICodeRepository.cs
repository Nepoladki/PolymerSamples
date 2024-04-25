using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ICodeRepository
    {
        ICollection<Code> GetCodes();
        Code GetCode(Guid id);
        bool CodeExists(Guid id);
        bool CreateCode(Code code);
        bool UpdateCode(Code code);
        bool DeleteCode(Code code);
        bool Save();

    }
}
