namespace PolymerSamples.DTO
{
    public class IncludedCodesDTO
    {
        public Guid code_in_vault_id { get; set; }
        public Guid code_id { get; set; }
        public required string short_code_name { get; set; }

        public string code_name { get; set; }
    }
}
