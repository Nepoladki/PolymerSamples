using Microsoft.EntityFrameworkCore;
using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.DTO;

namespace PolymerSamples.Repository
{
    public class CodeRepository : ICodeRepository
    {
        private readonly DataContext _context;
        public CodeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<CodeIncludesVaultsDTO>> GetAllCodesIncludingVaultsAsync()
        {
            return await _context.Codes
                .AsNoTracking()
                .Select(c => new CodeIncludesVaultsDTO
                {
                    id = c.Id,
                    code_index = c.CodeIndex,
                    code_name = c.CodeName,
                    legacy_code_name = c.LegacyCodeName,
                    stock_level = c.StockLevel,
                    in_vaults = c.CodeVaults
                        .Select(cv => new IncludedVaultsDTO
                        {
                            vault_id = cv.VaultId,
                            vault_name = cv.Vault.VaultName.ToString()
                        })
                        .ToList(),
                    layers = c.Layers,
                    thickness = c.Thickness,
                    type = c.SampleType.TypeName,
                    note = c.Note
                })
                .OrderBy(c => c.code_index)
                .ToListAsync();
        }

        public async Task<Codes> GetCodeByIdAsync(Guid id)
        {
            return await _context.Codes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id); 
        }
        public async Task<Codes?> GetCodeByNameAsync(string name)
        {
            return await _context.Codes.AsNoTracking().FirstOrDefaultAsync(c => c.CodeName == name.Trim());
        }
        public async Task<bool> CodeExistsAsync(Guid id)
        {
            return await _context.Codes.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> CreateCodeAsync(Codes code)
        {
            _context.Add(code);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> UpdateCodeAsync(Codes code)
        {
            _context.Update(code);
            return await SaveAsync();
        }

        public async Task<bool> DeleteCodeAsync(Codes code)
        {
            _context.Remove(code);
            return await SaveAsync();
        }
    }
}
