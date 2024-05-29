﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.Repository;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/vaults/")]
    [ApiController]
    [Authorize]
    public class VaultController : ControllerBase 
    {
        private readonly IVaultRepository _vaultRepository;
        public VaultController(IVaultRepository vaultRepository)
        {
            _vaultRepository = vaultRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VaultIncludesCodesDTO>))]
        public async Task<IActionResult> GetAllVaultsAsync()
        {
            var vaults = await _vaultRepository.GetAllVaultsAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vaults);
        }

        [HttpGet("{vaultId}")]
        [ProducesResponseType(200, Type = typeof(Vaults))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCodeVaultWithCodesAndVaultsByVaultId(Guid vaultId)
        {
            if (!await _vaultRepository.VaultExistsAsync(vaultId))
                return NotFound();

            var vault = await _vaultRepository.GetVaultWithCodesAndCivIdAsync(vaultId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vault);
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateVaultAsync([FromBody] VaultDTO newVault)
        {
            if (newVault is null)
                return BadRequest(ModelState);

            var existingVault = await _vaultRepository.GetVaultByNameAsync(newVault.vault_name);

            if (existingVault is not null)
            {
                return StatusCode(422, "Vault already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vault = DTOToModelExtensions.FromDTO(newVault);

            if (!await _vaultRepository.CreateVaultAsync(vault))
            {
                ModelState.AddModelError("", "Saving Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created and saved new vault");
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpDelete("{vaultId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVault(Guid vaultId)
        {
            if (!await _vaultRepository.VaultExistsAsync(vaultId))
                return NotFound();

            var vaultToDelete = await _vaultRepository.GetVaultByIdAsync(vaultId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _vaultRepository.DeleteVaultAsync(vaultToDelete))
            {
                ModelState.AddModelError("", $"Error occured while deleting vault with ID {vaultId}");
                return BadRequest(ModelState);
            }

            return Ok($"Successfully deleted vault with ID {vaultId}");
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpPut("{vaultId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCodeAsync(Guid vaultId, [FromBody] VaultDTO vaultDto)
        {
            if (!await _vaultRepository.VaultExistsAsync(vaultId))
                return NotFound("Vault does not exist in database");

            var vaultToUpdate = await _vaultRepository.GetVaultByIdAsync(vaultId);

            vaultToUpdate.VaultName = vaultDto.vault_name;
            vaultToUpdate.Note = vaultDto.note;

            if (!await _vaultRepository.UpdateVaultAsync(vaultToUpdate))
                return BadRequest($"Error occured while updating vault with ID {vaultId}");

            return Ok($"Succsessfully updated vault with ID {vaultId}");
        }
    }
}
