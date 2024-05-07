using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    [Table("users")]
    public class Users
    {
        [Column("id")] public Guid Id { get; set; }
        [Column("username")][Required] public string UserName { get; set; }
        [Column("password")] public string Password { get; set; }
        [Column("roles")][Required] public List<string> Roles { get; set; }
        [Column("is_active")] public bool IsActive { get; set; }
    }
}
