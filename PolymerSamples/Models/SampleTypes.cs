using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    [Table("sample_types")]
    public class SampleTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("type_name")] [Required] public required string TypeName { get; set; }
        public ICollection<Codes> Code { get; set; }
    }
}
