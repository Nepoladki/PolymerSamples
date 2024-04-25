using Microsoft.Extensions.Diagnostics.HealthChecks;
using PolymerSamples.Models;

namespace PolymerSamples.DTO
{
    public static class DTOToModel
    {
        public static Code FromDTO(this CodeDTO codeDto)
        {
            return new Code()
            {
                Id = codeDto.Id,
                CodeIndex = codeDto.CodeIndex,
                CodeName = codeDto.CodeName,
                LegacyCodeName = codeDto.LegacyCodeName,
                StockLevel = codeDto.StockLevel ?? "empty",
                Note = codeDto.Note
            };
        }
        public static Vault FromDTO(this VaultDTO vaultDto) 
        {
            return new Vault()
            {
                Id = vaultDto.Id,
                VaultName = vaultDto.VaultName,
                Note = vaultDto.Note
            };
        }
        public static CodeVault FromDTO(this CodeVaultDTO codeVaultDto)
        {
            return new CodeVault()
            {
                Id = codeVaultDto.Id == Guid.Empty ? Guid.NewGuid() : codeVaultDto.Id,
                CodeId = codeVaultDto.CodeId,
                VaultId = codeVaultDto.VaultId
            };
        }
    }
}
