using Microsoft.AspNetCore.Mvc;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/codesinvaults/[Controller]")]
    [ApiController]
    public class CodeVaultController : Controller
    {
        private readonly ICodeVaultRepository _codeVaultRepository;
        public CodeVaultController(ICodeVaultRepository codeVaultRepository)
        {
            _codeVaultRepository = codeVaultRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CodeVault>))]
        public IActionResult GetCodeVaults() 
        {
            var codeVault = _codeVaultRepository.GetCodeVaults().Select(c => c.AsDTO());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codeVault);
        }

        [HttpGet("{codeVaultId}")]
        [ProducesResponseType(200, Type = typeof(CodeVault))]
        [ProducesResponseType(400)]
        public IActionResult GetCodeVault(Guid codeVaultId)
        {
            if (!_codeVaultRepository.CodeVaultExists(codeVaultId))
                return NotFound();

            var codeVault = _codeVaultRepository.GetCodeVault(codeVaultId).AsDTO();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codeVault);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCodeVault(CodeVaultDTO newCodeVault)
        {
            //var existingCodeVault = _codeVaultRepository.GetCodeVault(newCodeVault.Id);

            //if (existingCodeVault != null)
            //{
                //ModelState.AddModelError("", "CodeVault realtion with this ID already exists");
                //return BadRequest(existingCodeVault);
            //}

            if(newCodeVault is null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var codeVault = DTOToModel.FromDTO(newCodeVault);

            if (!_codeVaultRepository.CreateCodeVault(codeVault))
            {
                ModelState.AddModelError("", "Saving Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created and saved new code and vault relation");
        }
    }
}
