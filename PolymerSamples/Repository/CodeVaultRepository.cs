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
        public ICollection<CodeVault> GetCodeVaults()
        {
            return _context.CodeVaults.OrderBy(cv => cv.Id).ToList();
        }

        public CodeVault GetCodeVault(Guid id)
        {
            return _context.CodeVaults.Where(cv => cv.Id == id).FirstOrDefault();
        }

        public bool CodeVaultExists(Guid id)
        {
            return _context.CodeVaults.Any(cv => cv.Id == id);
        }

        public bool CreateCodeVault(CodeVault codeVault)
        {
            _context.Add(codeVault);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool DeleteCodeVault(CodeVault codeVault)
        {
            _context.Remove(codeVault);
            return Save();
        }

        public bool UpdateCodeVault(CodeVault codeVault)
        {
            _context.Update(codeVault);
            return Save();
        }
    }
}
