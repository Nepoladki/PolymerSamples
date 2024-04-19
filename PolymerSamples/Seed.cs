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
                        Code = new Code()
                        {
                            Id = "0cbe6071-9f4d-426b-b3d9-fdcc8dbb9ea4",
                            CodeIndex = "1.1.10.01",
                            CodeName =  "1/10 6 U0/V5 70A SM Black A (CH)",
                            LegacyCodeName = "1M6U0-V5N",
                            StockLevel = "low",
                            Note = "12334"
                        },

                        Vault = new Vault()
                        {
                            Id = "bdd73b3b-8a96-4897-b55c-f25f2203b35c",
		                    VaultName = "E3",
		                    Note = "Моя заметка"
                        }
                    },
                    new CodeVault()
                    {
                        Code = new Code()
                        {
                            Id = "1bdds6071-7f4d-426b-b3d9-fdcc8dnb5da9",
                            CodeIndex = "1.3.40.01",
                            CodeName =  "3/40 15 U0/V10 S White (AS)",
                            LegacyCodeName = "3/40 S/B W FG",
                            StockLevel = "low",
                            Note = "12134"
                        },

                        Vault = new Vault()
                        {
                            Id = "ndf73b5b-9a94-4187-b15c-f25f2103t94d",
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
