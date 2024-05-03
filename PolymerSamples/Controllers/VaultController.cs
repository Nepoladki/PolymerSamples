﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using PolymerSamples.Repository;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/vaults/[controller]")]
    [ApiController]
    public class VaultController : Controller
    {
        private readonly IVaultRepository _vaultRepository;
        public VaultController(IVaultRepository vaultRepository)
        {
            _vaultRepository = vaultRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<VaultsIncludesCodesDTO>))]
        public IActionResult GetVaults()
        {
            var vaults = _vaultRepository.GetVaults();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vaults);
        }

        [HttpGet("{vaultId}")]
        [ProducesResponseType(200, Type = typeof(Vaults))]
        [ProducesResponseType(400)]
        public IActionResult GetVaultWithCodes(Guid vaultId)
        {
            if (!_vaultRepository.VaultExists(vaultId))
                return NotFound();

            var vault = _vaultRepository.GetVaultWithCodes(vaultId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(vault);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        public IActionResult CreateVault([FromBody] VaultDTO newVault)
        {
            if (newVault is null)
                return BadRequest(ModelState);

            var existingVault = _vaultRepository.GetVaults()
                .Where(v => v.vault_name.Trim().ToUpper() == newVault.VaultName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (existingVault != null)
            {
                ModelState.AddModelError("", "Vault already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vault = DTOToModel.FromDTO(newVault);

            if (!_vaultRepository.CreateVault(vault))
            {
                ModelState.AddModelError("", "Saving Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created and saved new vault");
        }

        [HttpDelete("{vaultId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteVault(Guid vaultId)
        {
            if (!_vaultRepository.VaultExists(vaultId))
                return NotFound();

            var vaultToDelete = _vaultRepository.GetVault(vaultId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_vaultRepository.DeleteVault(vaultToDelete))
            {
                ModelState.AddModelError("", $"Error occured while deleting vault with ID {vaultId}");
                return BadRequest(ModelState);
            }

            return Ok($"Successfully deleted vault with ID {vaultId}");
        }

        [HttpPatch("{vaultId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCode(Guid vaultId, [FromBody] JsonPatchDocument<Vaults> patchVault)
        {
            if (!_vaultRepository.VaultExists(vaultId))
                return NotFound();

            var vaultToUpdate = _vaultRepository.GetVault(vaultId);

            patchVault.ApplyTo(vaultToUpdate, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_vaultRepository.UpdateVault(vaultToUpdate))
            {
                ModelState.AddModelError("", $"Error occured while updating code with ID {vaultId}");
                return BadRequest(ModelState);
            }

            return Ok($"Succsessfully updated code with ID {vaultId}");
        }
    }
}