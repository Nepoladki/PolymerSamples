using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;

namespace PolymerSamples.Repository
{
    public class VaultRepository : IVaultRepository
    {
        private readonly DataContext _context;
        public VaultRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Vaults> GetVaults()
        {
            return _context.Vaults.OrderBy(p => p.Id).ToList();
        }

        public Vaults GetVault(Guid id)
        {
            return _context.Vaults.Where(v => v.Id == id).FirstOrDefault();
        }

        public bool VaultExists(Guid id)
        {
            return _context.Vaults.Any(v => v.Id == id);
        }

        public bool CreateVault(Vaults vault)
        {
            _context.Add(vault);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

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
