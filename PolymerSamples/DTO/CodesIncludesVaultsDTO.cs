namespace PolymerSamples.DTO
{
    public record class CodesIncludesVaultsDTO
    {
        public Guid id { get; set; }
        public string code_index { get; set; }
        public string code_name { get; set; }
        public string? legacy_code_name { get; set; }
        public string? stock_level { get; set; }
        public int layers { get; set; }
        public float thickness { get; set; }
        public string type { get; set; }
        public ICollection<IncludedVaultsDTO>? in_vaults { get; set; }
        public string? note { get; set; }
    }

}
