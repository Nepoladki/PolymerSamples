namespace PolymerSamples.DTO
{
    public class VaultWithAllIncludingDataDTO
    {
        public Guid code_vault_id { get; set; }
        public ICollection<IncludedCodesDTO> code_data { get; set; }
        public ICollection<IncludedVaultsDTO> vault_data { get; set; }
    }
}
