namespace PolymerSamples.Models
{
    public class CodeVault
    {
        public string Id { get; set; }
        public string CodeId { get; set; }
        public string VaultId { get; set; }
        public Vault Vault { get; set; }
        public Code Code { get; set; }
    }
}
