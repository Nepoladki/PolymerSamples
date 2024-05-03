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
        public ICollection<VaultsIncludesCodesDTO> GetVaults()
        {
            return _context.Vaults
                .Select(v => new VaultsIncludesCodesDTO
                {
                    id = v.Id,
                    vault_name = v.VaultName,
                    note = v.Note,
                    includes = v.CodeVaults
                        .Select(cv => new IncludedCodesDTO
                        {
                            id = cv.CodeId,
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
        public VaultsIncludesCodesDTO GetVaultWithCodes(Guid id)
        {
            return _context.Vaults.Where(v => v.Id == id)
                .Select(v => new VaultsIncludesCodesDTO
                {
                    id = v.Id,
                    vault_name = v.VaultName,
                    note = v.Note,
                    includes = v.CodeVaults.Select(cv => new IncludedCodesDTO
                    {
                        id = cv.CodeId,
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
    }
}
