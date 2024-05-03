namespace PolymerSamples.DTO
{
    public class VaultsIncludesCodesDTO
    {
        public Guid id { get; set; }
        public string vault_name { get; set; }
        public string note { get; set; }
        public ICollection<IncludedCodesDTO> includes { get; set; }
    }
}
