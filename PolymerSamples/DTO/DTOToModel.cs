using PolymerSamples.Authorization;
using PolymerSamples.Models;

namespace PolymerSamples.DTO
{
    public static class DTOToModel
    {
        public static Codes FromDTO(this CodeDTO codeDto)
        {
            return new Codes()
            {
                Id = codeDto.Id,
                CodeIndex = codeDto.CodeIndex,
                CodeName = codeDto.CodeName.Trim(),
                LegacyCodeName = codeDto.LegacyCodeName.Trim(),
                StockLevel = codeDto.StockLevel ?? "empty",
                Note = codeDto.Note,
                TypeId = codeDto.TypeId,
                Layers = codeDto.Layers,
                Thickness = codeDto.Thickness
            };
        }
        public static Vaults FromDTO(this VaultDTO vaultDto) 
        {
            return new Vaults()
            {
                Id = vaultDto.Id == Guid.Empty ? Guid.NewGuid() : vaultDto.Id,
                VaultName = vaultDto.VaultName.Trim(),
                Note = vaultDto.Note
            };
        }
        public static CodesVaults FromDTO(this CodeVaultDTO codeVaultDto)
        {
            return new CodesVaults()
            {
                Id = codeVaultDto.Id == Guid.Empty ? Guid.NewGuid() : codeVaultDto.Id,
                CodeId = codeVaultDto.CodeId,
                VaultId = codeVaultDto.VaultId
            };
        }
        public static Users FromDTO(this UserWithPasswordDTO userDto, string passwordHash)
        {
            return new Users()
            {
                Id = userDto.Id == Guid.Empty ? Guid.NewGuid() : userDto.Id,
                UserName = userDto.UserName.Trim(),
                HashedPassword = passwordHash,
                Role = userDto.Role ?? "user",
                IsActive = userDto.IsActive,
            };
        }
    }
}
