 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PolymerSamples.Authorization;

namespace PolymerSamples.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        [Required] public required string UserName { get; set; }
        [Required] public required string HashedPassword { get; set; }
        [Required] public required string Role { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshExpires { get; set; }
    }
}
