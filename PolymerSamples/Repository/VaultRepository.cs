using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.DTO;

namespace PolymerSamples.Repository
{
    public class VaultRepository : IVaultRepository
    {
        private readonly DataContext _context;
        public VaultRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<VaultIncludesCodesDTO> GetAllVaults()
        {
            return _context.Vaults
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
                .ToList();
        }
        public Vaults GetVault(Guid id)
        {
            return _context.Vaults.Where(v => v.Id == id).FirstOrDefault();
        }
        public VaultIncludesCodesDTO GetVaultWithCodes(Guid id)
        {
            return _context.Vaults.Where(v => v.Id == id)
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
                }).FirstOrDefault();
        }

        public bool VaultExists(Guid id) => _context.Vaults.Any(v => v.Id == id);

        public bool CreateVault(Vaults vault)
        {
            _context.Add(vault);
            return Save();
        }

        public bool Save() => _context.SaveChanges() > 0;

        public bool DeleteVault(Vaults vault)
        {
            _context.Remove(vault);
            return Save();
        }

        public bool UpdateVault(Vaults vault)
        {
            _context.Update(vault);
            return Save();
        }
        public ICollection<VaultIncludesCodesDTO>? GetVaultWithCodesAndCivId(Guid vaultId)
        {
            return _context.Vaults
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
                .ToList();
        }
    }
}
