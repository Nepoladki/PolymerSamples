using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    [Table("codes_in_vaults")]
    public class CodesVaults
    {
        [Column("id")] public Guid Id { get; set; } = Guid.NewGuid();
        [Column("code_id")] [Required] public Guid CodeId { get; set; }
        [Column("vault_id")] [Required] public Guid VaultId { get; set; }
        public Vaults Vault { get; set; }
        public Codes Code { get; set; }
    }
}
