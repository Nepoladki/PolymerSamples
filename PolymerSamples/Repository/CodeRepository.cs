﻿using Microsoft.EntityFrameworkCore;
using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.DTO;
using System.Text.RegularExpressions;
using System.Net;

namespace PolymerSamples.Repository
{
    public class CodeRepository : ICodeRepository
    {
        private readonly DataContext _context;
        public CodeRepository(DataContext context)
        {
            _context = context;
        }
        /*public ICollection<CodesWithVaultsDTO> GetCodes()
        {
            var listRes = new List<CodesWithVaultsDTO>();

            var queryResult = _context.Codes
                .FromSql(@$"SELECT
                                c.id,
                                c.code_index,
                                c.code_name,
                                c.legacy_code_name,
                                c.stock_level,
                                json_agg(json_build_object(
                                    'vault_id', v.id,
                                    'vault_name', v.vault_name
                                )) AS in_vaults,
                                c.note
                            FROM
                                codes c
                            LEFT JOIN
                                codes_in_vaults cv ON c.id = cv.code_id
                            LEFT JOIN
                                vaults v ON cv.vault_id = v.id
                            GROUP BY
                                c.id,
                                c.code_index,
                                c.code_name,
                                c.legacy_code_name,
                                c.stock_level,
                                c.note
                            ORDER BY
                                c.code_index ASC;").AsNoTracking().ToList();

            return queryResult;
        }*/
        public ICollection<CodesWithVaultsDTO> GetCodes()
        {
            return _context.Codes
                .Select(c => new CodesWithVaultsDTO
                {
                    id = c.Id,
                    code_index = c.CodeIndex,
                    code_name = c.CodeName,
                    legacy_code_name = c.LegacyCodeName,
                    stock_level = c.StockLevel,
                    in_vaults = c.CodeVaults
                        .Select(cv => new InnerVaultsDTO
                        {
                            vault_id = cv.VaultId,
                            vault_name = cv.Vault.VaultName.ToString()
                        })
                        .ToList(),
                    note = c.Note
                })
                .OrderBy(c => c.code_index)
                .ToList();
        }

        public Codes GetCode(Guid id)
        {
            return _context.Codes.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool CodeExists(Guid id)
        {
            return _context.Codes.Any(c => c.Id == id);
        }

        public bool CreateCode(Codes code)
        {
            _context.Add(code);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCode(Codes code)
        {
            _context.Update(code);
            return Save();
        }

        public bool DeleteCode(Codes code)
        {
            _context.Remove(code);
            return Save();
        }
    }
}
