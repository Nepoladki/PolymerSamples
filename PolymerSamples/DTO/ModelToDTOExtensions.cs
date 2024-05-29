using PolymerSamples.Models;

namespace PolymerSamples.DTO
{
    public static class ModelToDTOExtensions
    {
        public static CodeDTO AsDTO(this Codes code)
        {
            return new CodeDTO
            (
                code.Id,
                code.ShortCodeName,
                code.CodeName,
                code.SupplierCodeName,
                code.StockLevel,
                code.Note,
                code.TypeId,
                code.Layers,
                code.Thickness
            );
        }
        public static VaultDTO AsDTO(this Vaults vault) 
        {
            return new VaultDTO
            (
                vault.Id,
                vault.VaultName,
                vault.Note
            );
        }
        public static CodeVaultDTO AsDTO(this CodesVaults codeVault)
        {
            return new CodeVaultDTO
            (
                codeVault.Id,
                codeVault.CodeId,
                codeVault.VaultId
            );
        }
        public static UserDTO AsDTO(this Users user)
        {
            return new UserDTO
            (
                user.Id,
                user.UserName,
                user.Role,
                user.IsActive,
                user.RefreshToken,
                user.RefreshExpires
            );
        }
    }
}
