using Microsoft.AspNetCore.Authorization;
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

            return Ok(vaults);
        }

        [HttpGet("{vaultId}")]
        [ProducesResponseType(200, Type = typeof(Vaults))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCodeVaultWithCodesAndVaultsByVaultId(Guid vaultId)
        {
            if (!await _vaultRepository.VaultExistsAsync(vaultId))
                return NotFound($"Vault with id {vaultId} does not exist");

            var vault = await _vaultRepository.GetVaultWithCodesAndCivIdAsync(vaultId);

            return Ok(vault);
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateVaultAsync([FromBody] VaultDTO newVault)
        {
            if (newVault is null)
                return BadRequest("Vault object does not exist");

            var existingVault = await _vaultRepository.GetVaultByNameAsync(newVault.vault_name);

            if (existingVault is not null)
                return StatusCode(422, "Vault already exists");

            var vault = DTOToModelExtensions.FromDTO(newVault);

            if (!await _vaultRepository.CreateVaultAsync(vault))
                return StatusCode(500, "Error occured while saving new vault to database");

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

            if (!await _vaultRepository.DeleteVaultAsync(vaultToDelete))
                return BadRequest($"Error occured while deleting vault with ID {vaultId}");

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
