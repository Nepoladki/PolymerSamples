using Microsoft.EntityFrameworkCore;
using PolymerSamples.Models;

namespace PolymerSamples.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Codes> Codes { get; set; }
        public DbSet<Vaults> Vaults { get; set; }
        public DbSet<CodesVaults> CodesVaults { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodesVaults>()
                .HasKey(cv => cv.Id);
            modelBuilder.Entity<CodesVaults>()
                .HasOne(cv => cv.Code)
                .WithMany(cv => cv.CodeVaults)
                .HasForeignKey(cv => cv.CodeId);
            modelBuilder.Entity<CodesVaults>()
                .HasOne(cv => cv.Vault)
                .WithMany(cv => cv.CodeVaults)
                .HasForeignKey(cv => cv.VaultId);
        }
    }
}
