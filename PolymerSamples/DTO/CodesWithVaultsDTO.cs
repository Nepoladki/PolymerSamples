namespace PolymerSamples.DTO
{
    public record class CodesWithVaultsDTO
    {
        public Guid id { get; set; }
        public string code_index { get; set; }
        public string code_name { get; set; }
        public string? legacy_code_name { get; set; }
        public string? stock_level { get; set; }  
        public ICollection<InnerVaultsDTO>? in_vaults { get; set; }
        public string? note { get; set; }
    }

}
