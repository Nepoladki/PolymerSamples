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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeVault>()
                .HasKey(cv => cv.Id);
            modelBuilder.Entity<CodeVault>()
                .HasOne(cv => cv.Code)
                .WithMany(cv => cv.CodeVaults)
                .HasForeignKey(cv => cv.CodeId);
            modelBuilder.Entity<CodeVault>()
                .HasOne(cv => cv.Vault)
                .WithMany(cv => cv.CodeVaults)
                .HasForeignKey(cv => cv.VaultId);
        }
    }
}
