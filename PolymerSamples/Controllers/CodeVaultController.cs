using Microsoft.AspNetCore.Mvc;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Authorization;

namespace PolymerSamples.Controllers
{
    [Route("api/codesinvaults/")]
    [ApiController]
    [Authorize]
    public class CodeVaultController : ControllerBase
    {
        private readonly ICodeVaultRepository _codeVaultRepository;
        public CodeVaultController(ICodeVaultRepository codeVaultRepository)
        {
            _codeVaultRepository = codeVaultRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CodesVaults>))]
        public async Task<IActionResult> GetAllCodeVaults()
        {
            var codeVaults = await _codeVaultRepository.GetAllCodeVaultsAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codeVaults.Select(c => c.AsDTO()));
        }

        [HttpGet("{codeVaultId}")]
        [ProducesResponseType(200, Type = typeof(CodesVaults))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCodeVaultAsync(Guid codeVaultId)
        {
            if (!await _codeVaultRepository.CodeVaultExistsAsync(codeVaultId))
                return NotFound();

            var codeVault = await _codeVaultRepository.GetCodeVaultByIdAsync(codeVaultId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codeVault.AsDTO());
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCodeVaultAsync(CodeVaultDTO newCodeVault)
        {
            if (newCodeVault is null)
                return BadRequest("Invalid code-vault relationship object");

            var codeVault = DTOToModelExtensions.FromDTO(newCodeVault);

            if (!await _codeVaultRepository.CreateCodeVaultAsync(codeVault))
                return StatusCode(500, $"Error occured while saving code-vault realtionship with id {codeVault.Id}");

            return Ok("Succesfully created and saved new code and vault relation");
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpDelete("{codeVaultId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCodeVaultAsync(Guid codeVaultId)
        {
            if (!await _codeVaultRepository.CodeVaultExistsAsync(codeVaultId))
                return NotFound($"Code-vault relationship with id {codeVaultId} does not exist");

            var codeVaultToDelete = await _codeVaultRepository.GetCodeVaultByIdAsync(codeVaultId);

            if (!await _codeVaultRepository.DeleteCodeVaultAsync(codeVaultToDelete))
                return StatusCode(500, $"Error occured while deleting code-vault realtionship with ID {codeVaultId}");

            return Ok($"Succsessfully deleted code with ID {codeVaultId}");
        }

        [Authorize(Policy = AuthData.AdminPolicyName)]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCodeVaultAsync(Guid id, CodeVaultDTO codeVaultDTO)
        {
            if (codeVaultDTO is null)
                return BadRequest("Invalid code-vault relationship object");

            CodesVaults codeVault = await _codeVaultRepository.GetCodeVaultByIdAsync(id);

            if (codeVault is null)
                return NotFound($"Did not found code-vault relationship with id {id}");

            codeVault.VaultId = codeVaultDTO.vault_id;
            codeVault.CodeId = codeVaultDTO.code_id;

            if (!await _codeVaultRepository.UpdateCodeVaultAsync(codeVault))
                return StatusCode(500, "Error occured while saving updated code-vault relationship to database");

            return Ok($"Succsessfully updated code-vault realationship with id {id}");
        }
    }
}
