using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    public class SampleTypes
    {
        public int Id { get; set; }
        [Required] public required string TypeName { get; set; }
        public ICollection<Codes> Code { get; set; }
    }
}
