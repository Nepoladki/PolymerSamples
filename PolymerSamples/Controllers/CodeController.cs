using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/codes/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class CodeController : ControllerBase
    {
        private readonly ICodeRepository _codesRepository;
        public CodeController(ICodeRepository codesRepository)
        {
            _codesRepository = codesRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CodeIncludesVaultsDTO>))]
        public async Task<IActionResult> GetAllCodesAsync()
        {
            var codes = await _codesRepository.GetAllCodesIncludingVaultsAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codes);
        }

        [HttpGet("{codeId}")]
        [ProducesResponseType(200, Type = typeof(Codes))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCodeAsync(Guid codeId) 
        {
            if (!await _codesRepository.CodeExistsAsync(codeId))
                return NotFound();

            var code =  await _codesRepository.GetCodeByIdAsync(codeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(code.AsDTO());
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCodeAsync([FromBody] CodeDTO newCode) 
        {
            if(newCode is null)
                return BadRequest(ModelState);

            var existingCode = await _codesRepository.GetCodeByNameAsync(newCode.CodeName);

            if (existingCode is not null)
                return StatusCode(409, $"Code with this name already exists, its id is {existingCode.Id}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var code = DTOToModel.FromDTO(newCode);
            
            if (!await _codesRepository.CreateCodeAsync(code))
                return StatusCode(500, "Error occured while saving data to database");

            return Ok("Succesfully created and saved new code");
        }

        [HttpDelete("{codeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCode(Guid codeId)
        {
            if(!await _codesRepository.CodeExistsAsync(codeId))
                return NotFound();

            var codeToDelete = await _codesRepository.GetCodeByIdAsync(codeId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _codesRepository.DeleteCodeAsync(codeToDelete))
            {
                ModelState.AddModelError("", $"Error occured while deleting code with ID {codeId}");
                return BadRequest(ModelState);
            }

            return Ok($"Succsessfully deleted code with ID {codeId}");
        }

        [HttpPatch("{codeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCodeAsync(Guid codeId, [FromBody] JsonPatchDocument<Codes> patchCode)
        {
            if (!await _codesRepository.CodeExistsAsync(codeId))
                return NotFound();

            var codeToUpdate = await _codesRepository.GetCodeByIdAsync(codeId);

            patchCode.ApplyTo(codeToUpdate, ModelState);
      
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _codesRepository.UpdateCodeAsync(codeToUpdate))
            {
                ModelState.AddModelError("", $"Error occured while updating code with ID {codeId}");
                return BadRequest(ModelState);
            }

            return Ok($"Succsessfully updated code with ID {codeId}");
        }
    }

}
