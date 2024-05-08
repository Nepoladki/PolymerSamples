using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    [Table("vaults")]
    public class Vaults
    {
        [Column("id")] public Guid Id { get; set; }
        [Column("vault_name")][Required] public string VaultName { get; set; }
        [Column("note")] public string? Note { get; set; }
        public ICollection<CodesVaults> CodeVaults { get; set; }
    }
}
