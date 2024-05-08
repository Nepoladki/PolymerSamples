using Microsoft.EntityFrameworkCore;
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
        public async Task<ICollection<CodesVaults>> GetAllCodeVaultsAsync()
        {
            return await _context.CodesVaults.OrderBy(cv => cv.Id).ToListAsync();
        }

        public async Task<CodesVaults> GetCodeVaultByIdAsync(Guid id)
        {
            return await _context.CodesVaults.Where(cv => cv.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> CodeVaultExistsAsync(Guid id)
        {
            return await _context.CodesVaults.AnyAsync(cv => cv.Id == id);
        }

        public async Task<bool> CreateCodeVaultAsync(CodesVaults codeVault)
        {
            _context.Add(codeVault);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCodeVaultAsync(CodesVaults codeVault)
        {
            _context.Remove(codeVault);
            return await SaveAsync();
        }
    }
}
