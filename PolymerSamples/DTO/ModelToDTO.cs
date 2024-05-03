﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using PolymerSamples.Models;
using System.Diagnostics;

namespace PolymerSamples.DTO
{
    public static class ModelToDTO
    {
        public static CodeDTO AsDTO(this Codes code)
        {
            return new CodeDTO
            (
                code.Id,
                code.CodeIndex,
                code.CodeName,
                code.LegacyCodeName,
                code.StockLevel,
                code.Note
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
    }
}