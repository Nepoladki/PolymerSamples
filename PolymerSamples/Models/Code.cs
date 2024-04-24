using System.ComponentModel.DataAnnotations;

namespace PolymerSamples.Models
{
    public class Code
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public string CodeIndex { get; set; }
        [Required] public string CodeName { get; set; }
        public string? LegacyCodeName { get; set; }
        public string? StockLevel { get; set; } = "empty";
        public string? Note { get; set; }
        public ICollection<CodeVault> CodeVaults { get; set; }
    }
}
