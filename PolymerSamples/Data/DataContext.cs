using Microsoft.EntityFrameworkCore;
using PolymerSamples.Models;

namespace PolymerSamples.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Code> Codes { get; set; }
        public DbSet<Vault> Vaults { get; set; }
        public DbSet<CodeVault> CodeVaults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeVault>()
                .`(pc => new { pc.VaultId, pc.CodeId } );
            modelBuilder.Entity<CodeVault>()
                .HasOne(p => p.Code)
                .WithMany(pc => pc.CodeVaults)
                .HasForeignKey(c => c.CodeId);
            modelBuilder.Entity<CodeVault>()
                .HasOne(p => p.Vault)
                .WithMany(pc => pc.CodeVaults)
                .HasForeignKey(c => c.VaultId);
        }
    }
}
