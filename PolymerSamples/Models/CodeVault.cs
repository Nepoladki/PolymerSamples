namespace PolymerSamples.Models
{
    public class CodeVault
    {
        public Guid Id { get; set; }
        public Guid CodeId { get; set; }
        public Guid VaultId { get; set; }
        public Vault Vault { get; set; }
        public Code Code { get; set; }
    }
}
