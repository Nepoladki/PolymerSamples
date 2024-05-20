using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    public class Vaults
    {
        public Guid Id { get; set; }
        [Required] public string VaultName { get; set; }
        public string? Note { get; set; }
        public ICollection<CodesVaults> CodeVaults { get; set; }
    }
}
