using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    [Table("users")]
    public class Users
    {
        [Column("id")] public Guid Id { get; set; }
        [Column("name")][Required] public string Name { get; set; }
        [Column("password")] public string Password { get; set; }
        [Column("role")] public string Role { get; set; }
        [Column("is_active")] public bool IsActive { get; set; }
    }
}
