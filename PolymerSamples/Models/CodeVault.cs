using System.ComponentModel.DataAnnotations;

namespace PolymerSamples.Models
{
    public class CodeVault
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public Guid CodeId { get; set; }
        [Required] public Guid VaultId { get; set; }
        public Vault Vault { get; set; }
        public Code Code { get; set; }
    }
}
