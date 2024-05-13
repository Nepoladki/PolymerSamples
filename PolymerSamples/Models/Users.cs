 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PolymerSamples.Authorization;

namespace PolymerSamples.Models
{
    [Table("users")]
    public class Users
    {
        [Column("id")] public Guid Id { get; set; }
        [Column("username")][Required] public required string UserName { get; set; }
        [Column("password")][Required] public required string HashedPassword { get; set; }
        [Column("role")][Required] public string Role { get; set; }
        [Column("is_active")] public bool IsActive { get; set; }
    }
}
