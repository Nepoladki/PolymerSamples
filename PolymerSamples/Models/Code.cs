namespace PolymerSamples.Models
{
    public class Code
    {
        public Guid Id { get; set; }
        public string CodeIndex { get; set; }
        public string CodeName { get; set; }
        public string LegacyCodeName { get; set; }
        public string StockLevel { get; set; }
        public string Note { get; set; }
        public ICollection<CodeVault> CodeVaults { get; set; }
    }
}
