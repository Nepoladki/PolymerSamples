namespace PolymerSamples.DTO
{
    public class VaultIncludesCodesDTO
    {
        public Guid id { get; set; }
        public required string vault_name { get; set; }
        public required string note { get; set; }
        public ICollection<IncludedCodesDTO> includes { get; set; }
    }
}
