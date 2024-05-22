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

        [RequiresClaim(AuthData.RoleClaimType, AuthData.EditorClaimValue)]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCodeVaultAsync(CodeVaultDTO newCodeVault)
        {
            if (newCodeVault is null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var codeVault = DTOToModelExtensions.FromDTO(newCodeVault);

            if (!await _codeVaultRepository.CreateCodeVaultAsync(codeVault))
            {
                ModelState.AddModelError("", "Saving Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created and saved new code and vault relation");
        }

        [RequiresClaim(AuthData.RoleClaimType, AuthData.EditorClaimValue)]
        [HttpDelete("{codeVaultId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCodeVaultAsync(Guid codeVaultId)
        {
            if (!await _codeVaultRepository.CodeVaultExistsAsync(codeVaultId))
                return BadRequest(ModelState);

            var codeVaultToDelete = await _codeVaultRepository.GetCodeVaultByIdAsync(codeVaultId);

            if (!await _codeVaultRepository.DeleteCodeVaultAsync(codeVaultToDelete))
            {
                ModelState.AddModelError("", $"Error occured while deleting code with ID {codeVaultId}");
                return StatusCode(500, ModelState);
            }

            return Ok($"Succsessfully deleted code with ID {codeVaultId}");
        }
    }
}
