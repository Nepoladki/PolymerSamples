using PolymerSamples.Authorization;
using PolymerSamples.Models;

namespace PolymerSamples.DTO
{
    public static class DTOToModelExtensions
    {
        public static Codes FromDTO(this CodeDTO codeDto)
        {
            return new Codes()
            {
                Id = codeDto.id,
                ShortCodeName = codeDto.short_code_name,
                CodeName = codeDto.code_name.Trim(),
                SupplierCodeName = codeDto.supplier_code_name?.Trim(),
                StockLevel = codeDto.stock_level ?? "empty",
                Note = codeDto.note,
                TypeId = codeDto.type_id,
                Layers = codeDto.layers,
                Thickness = codeDto.thickness
            };
        }
        public static Vaults FromDTO(this VaultDTO vaultDto) 
        {
            return new Vaults()
            {
                Id = vaultDto.id == Guid.Empty ? Guid.NewGuid() : vaultDto.id,
                VaultName = vaultDto.vault_name.Trim(),
                Note = vaultDto.note
            };
        }
        public static CodesVaults FromDTO(this CodeVaultDTO codeVaultDto)
        {
            return new CodesVaults()
            {
                Id = codeVaultDto.id == Guid.Empty ? Guid.NewGuid() : codeVaultDto.id,
                CodeId = codeVaultDto.code_id,
                VaultId = codeVaultDto.vault_id
            };
        }
        public static Users FromDTO(this UserWithPasswordDTO userDto, string passwordHash)
        {
            return new Users()
            {
                Id = userDto.id == Guid.Empty ? Guid.NewGuid() : userDto.id,
                UserName = userDto.username.Trim(),
                HashedPassword = passwordHash,
                Role = userDto.role ?? "user",
                IsActive = userDto.is_active,
                RefreshToken = userDto.refresh_token,
                RefreshExpires = userDto.refresh_expires
            };
        }
    }
}
