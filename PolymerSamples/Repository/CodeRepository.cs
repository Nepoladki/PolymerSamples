using Microsoft.EntityFrameworkCore;
using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.DTO;
using System.Text.RegularExpressions;
using System.Net;

namespace PolymerSamples.Repository
{
    public class CodeRepository : ICodeRepository
    {
        private readonly DataContext _context;
        public CodeRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<CodesIncludesVaultsDTO> GetCodes()
        {
            return _context.Codes
                .Select(c => new CodesIncludesVaultsDTO
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
                .ToList();
        }

        public Codes GetCode(Guid id) => _context.Codes.Where(c => c.Id == id).FirstOrDefault();

        public bool CodeExists(Guid id)
        {
            return _context.Codes.Any(c => c.Id == id);
        }

        public bool CreateCode(Codes code)
        {
            _context.Add(code);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCode(Codes code)
        {
            _context.Update(code);
            return Save();
        }

        public bool DeleteCode(Codes code)
        {
            _context.Remove(code);
            return Save();
        }
    }
}
