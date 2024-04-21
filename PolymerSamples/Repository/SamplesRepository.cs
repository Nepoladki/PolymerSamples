using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;

namespace PolymerSamples.Repository
{
    public class SamplesRepository : ISamplesRepository
    {
        private readonly DataContext _context;

        public SamplesRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Code> GetCodes()
        {
            return _context.Codes.OrderBy(p => p.Id).ToList();
        }
    }
}
