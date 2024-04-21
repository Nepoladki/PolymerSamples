using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface ISamplesRepository
    {
        ICollection<Code> GetCodes();

    }
}
