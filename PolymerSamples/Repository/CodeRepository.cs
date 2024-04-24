using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;

namespace PolymerSamples.Repository
{
    public class CodeRepository : ICodeRepository
    {
        private readonly DataContext _context;
        public CodeRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Code> GetCodes()
        {
            return _context.Codes.OrderBy(p => p.Id).ToList();
        }

        public Code GetCode(Guid id)
        {
            return _context.Codes.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool CodeExists(Guid id)
        {
            return _context.Codes.Any(c => c.Id == id);
        }

        public bool CreateCode(Code code)
        {
            _context.Add(code);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
