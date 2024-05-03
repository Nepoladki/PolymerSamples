﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolymerSamples.Models
{
    [Table("codes")]
    public class Codes
    {
        [Column("id")] public Guid Id { get; set; } = Guid.NewGuid();
        [Column("code_index")] [Required] public string CodeIndex { get; set; }
        [Column("code_name")] [Required] public string CodeName { get; set; }
        [Column("legacy_code_name")] public string? LegacyCodeName { get; set; }
        [Column("stock_level")] public string? StockLevel { get; set; } = "empty";
        [Column("note")] public string? Note { get; set; }
        public ICollection<CodesVaults> CodeVaults { get; set; }
    }
}