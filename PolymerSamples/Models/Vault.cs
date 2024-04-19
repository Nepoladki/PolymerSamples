namespace PolymerSamples.Models
{
    public class Vault
    {
        public string Id { get; set; }
        public string VaultName { get; set; }
        public string Note { get; set; }
        public ICollection<CodeVault> CodeVaults { get; set; }
    }
}
