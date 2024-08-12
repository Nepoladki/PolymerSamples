using PolymerSamples.DTO;

namespace PolymerSamples.Interfaces;

public interface ICodeValidator
{
    bool ValidateCode(CodeDTO code);
}
