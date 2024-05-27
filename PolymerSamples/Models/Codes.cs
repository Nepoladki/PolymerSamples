using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    public class Codes
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] required public string CodeIndex { get; set; }
        [Required] required public string CodeName { get; set; }
        public string? SupplierCodeName { get; set; }
        public string? StockLevel { get; set; } = "empty";
        public string? Note { get; set; }
        public int? TypeId { get; set; }
        public int? Layers { get; set; }
        public float? Thickness { get; set; }
        public ICollection<CodesVaults> CodeVaults { get; set; }
        public SampleTypes SampleType { get; set; }
    }
}
