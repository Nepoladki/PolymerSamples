using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.DTO;
using Microsoft.EntityFrameworkCore;

namespace PolymerSamples.Repository
{
    public class VaultRepository : IVaultRepository
    {
        private readonly DataContext _context;
        public VaultRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<VaultIncludesCodesDTO>> GetAllVaultsAsync()
        {
            return await _context.Vaults
                .Select(v => new VaultIncludesCodesDTO
                {
                    id = v.Id,
                    vault_name = v.VaultName,
                    note = v.Note,
                    includes = v.CodeVaults
                        .Select(cv => new IncludedCodesDTO
                        {
                            code_id = cv.CodeId,
                            code_index = cv.Code.CodeIndex.ToString()
                        }).ToList()
                })
                .OrderBy(v => v.vault_name)
                .ToListAsync();
        }
        public async Task<Vaults> GetVaultByIdAsync(Guid id)
        {
            return await _context.Vaults.Where(v => v.Id == id).FirstOrDefaultAsync();
        }
        public async Task<VaultIncludesCodesDTO?> GetVaultWithCodesByIdAsync(Guid id)
        {
            return await _context.Vaults.Where(v => v.Id == id)
                .Select(v => new VaultIncludesCodesDTO
                {
                    id = v.Id,
                    vault_name = v.VaultName,
                    note = v.Note,
                    includes = v.CodeVaults.Select(cv => new IncludedCodesDTO
                    {
                        code_id = cv.CodeId,
                        code_index = cv.Code.CodeIndex.ToString()
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> VaultExistsAsync(Guid id) => _context.Vaults.Any(v => v.Id == id);

        public async Task<bool> CreateVaultAsync(Vaults vault)
        {
            _context.Add(vault);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> DeleteVaultAsync(Vaults vault)
        {
            _context.Remove(vault);
            return await SaveAsync();
        }

        public async Task<bool> UpdateVaultAsync(Vaults vault)
        {
            _context.Update(vault);
            return await SaveAsync();
        }
        public async Task<ICollection<VaultIncludesCodesDTO>?> GetVaultWithCodesAndCivIdAsync(Guid vaultId)
        {
            return await _context.Vaults
                .Where(v => v.Id == vaultId)
                .Select(v => new VaultIncludesCodesDTO
                {
                    id = v.CodeVaults.Select(cv => cv.Id).FirstOrDefault(),
                    vault_name = v.VaultName,
                    note = v.Note,
                    includes = v.CodeVaults
                        .Select(cv => new IncludedCodesDTO
                        {
                            code_id = cv.CodeId,
                            code_index = cv.Code.CodeIndex.ToString()
                        }).ToList()
                })
                .OrderBy(v => v.vault_name)
                .ToListAsync();
        }

        public async Task<Vaults?> GetVaultByNameAsync(string name)
        {
            return await _context.Vaults
                .Where(v => v.VaultName.Trim() == name.Trim())
                .FirstOrDefaultAsync();
        }
    }
}
