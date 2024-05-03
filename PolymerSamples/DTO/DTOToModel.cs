﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
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
                CodeName = codeDto.CodeName,
                LegacyCodeName = codeDto.LegacyCodeName,
                StockLevel = codeDto.StockLevel ?? "empty",
                Note = codeDto.Note
            };
        }
        public static Vaults FromDTO(this VaultDTO vaultDto) 
        {
            return new Vaults()
            {
                Id = vaultDto.Id,
                VaultName = vaultDto.VaultName,
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
    }
}