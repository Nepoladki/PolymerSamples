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
    public class CodeController : ControllerBase
    {
        private readonly ICodeRepository _codesRepository;
        public CodeController(ICodeRepository codesRepository)
        {
            _codesRepository = codesRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CodeIncludesVaultsDTO>))]
        public IActionResult GetAllCodes()
        {
            var codes = _codesRepository.GetAllCodesIncludingVaults();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codes);
        }

        [HttpGet("{codeId}")]
        [ProducesResponseType(200, Type = typeof(Codes))]
        [ProducesResponseType(400)]
        public IActionResult GetCode(Guid codeId) 
        {
            if (!_codesRepository.CodeExists(codeId))
                return NotFound();

            var code = _codesRepository.GetCodeById(codeId).AsDTO();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(code);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult CreateCode([FromBody] CodeDTO newCode) 
        {
            if(newCode is null)
                return BadRequest(ModelState);

            var existingCode = _codesRepository.GetCodeWithCurrentName(newCode.CodeName);

            if (existingCode is not null)
                return StatusCode(409, $"Code with this name already exists, its id is {existingCode.Id}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var code = DTOToModel.FromDTO(newCode);
            
            if (!_codesRepository.CreateCode(code))
                return StatusCode(500, "Error occured while saving data to database");

            return Ok("Succesfully created and saved new code");
        }

        [HttpDelete("{codeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCode(Guid codeId)
        {
            if(!_codesRepository.CodeExists(codeId))
                return NotFound();

            var codeToDelete = _codesRepository.GetCodeById(codeId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_codesRepository.DeleteCode(codeToDelete))
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
        public IActionResult UpdateCode(Guid codeId, [FromBody] JsonPatchDocument<Codes> patchCode)
        {
            if (!_codesRepository.CodeExists(codeId))
                return NotFound();

            var codeToUpdate = _codesRepository.GetCodeById(codeId);

            patchCode.ApplyTo(codeToUpdate, ModelState);
      
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_codesRepository.UpdateCode(codeToUpdate))
            {
                ModelState.AddModelError("", $"Error occured while updating code with ID {codeId}");
                return BadRequest(ModelState);
            }

            return Ok($"Succsessfully updated code with ID {codeId}");
        }
    }

}
