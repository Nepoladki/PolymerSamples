using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    public class CodesVaults
    {   
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public Guid CodeId { get; set; }
        [Required] public Guid VaultId { get; set; }
        public Vaults Vault { get; set; }
        public Codes Code { get; set; }
    }
}
