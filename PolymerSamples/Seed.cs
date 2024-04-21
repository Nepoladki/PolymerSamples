using PolymerSamples.Data;
using PolymerSamples.Models;

namespace PolymerSamples
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.CodeVaults.Any())
            {
                var codeVaults = new List<CodeVault>()
                {
                    new CodeVault()
                    {
                        Id = Guid.NewGuid(),

                        Code = new Code()
                        {
                            Id = Guid.NewGuid(),
                            CodeIndex = "1.1.10.01",
                            CodeName =  "1/10 6 U0/V5 70A SM Black A (CH)",
                            LegacyCodeName = "1M6U0-V5N",
                            StockLevel = "low",
                            Note = "12334"
                        },

                        Vault = new Vault()
                        {
                            Id = Guid.NewGuid(),
		                    VaultName = "E3",
		                    Note = "Моя заметка"
                        }
                    },
                    new CodeVault()
                    {
                        Id = Guid.NewGuid(),

                        Code = new Code()
                        {
                            Id = Guid.NewGuid(),
                            CodeIndex = "1.3.40.01",
                            CodeName =  "3/40 15 U0/V10 S White (AS)",
                            LegacyCodeName = "3/40 S/B W FG",
                            StockLevel = "low",
                            Note = "12134"
                        },

                        Vault = new Vault()
                        {
                            Id = Guid.NewGuid(),
                            VaultName = "E4",
                            Note = "Моя заметка"
                        }
                    }
                };
                dataContext.CodeVaults.AddRange(codeVaults);
                dataContext.SaveChanges();
            }
        }
    }
}
