using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;

namespace PolymerSamples.Repository
{
    public class CodeVaultRepository : ICodeVaultRepository
    {
        private readonly DataContext _context;
        public CodeVaultRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<CodesVaults> GetAllCodeVaults()
        {
            return _context.CodesVaults.OrderBy(cv => cv.Id).ToList();
        }

        public CodesVaults GetCodeVaultById(Guid id)
        {
            return _context.CodesVaults.Where(cv => cv.Id == id).FirstOrDefault();
        }

        public bool CodeVaultExists(Guid id)
        {
            return _context.CodesVaults.Any(cv => cv.Id == id);
        }

        public bool CreateCodeVault(CodesVaults codeVault)
        {
            _context.Add(codeVault);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool DeleteCodeVault(CodesVaults codeVault)
        {
            _context.Remove(codeVault);
            return Save();
        }
    }
}
