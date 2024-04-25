using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/codes/[controller]")]
    [ApiController]
    public class CodeController : Controller
    {
        private readonly ICodeRepository _codesRepository;
        public CodeController(ICodeRepository codesRepository)
        {
            _codesRepository = codesRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Code>))]
        public IActionResult GetCodes()
        {
            var codes = _codesRepository.GetCodes().Select(c => c.AsDTO());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codes);
        }

        [HttpGet("{codeId}")]
        [ProducesResponseType(200, Type = typeof(Code))]
        [ProducesResponseType(400)]
        public IActionResult GetCode(Guid codeId) 
        {
            if (!_codesRepository.CodeExists(codeId))
                return NotFound();

            var code = _codesRepository.GetCode(codeId).AsDTO();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(code);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCode([FromBody] CodeDTO newCode) 
        {
            if(newCode is null)
                return BadRequest(ModelState);

            var existingCode = _codesRepository.GetCodes()
                .Where(c => c.CodeName.Trim().ToUpper() == newCode.CodeName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (existingCode != null)
            {
                ModelState.AddModelError("", "Code already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var code = DTOToModel.FromDTO(newCode);
            
            if (!_codesRepository.CreateCode(code))
            {
                ModelState.AddModelError("", "Saving Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created and saved new code");
        }

        [HttpDelete("{codeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCode(Guid codeId)
        {
            if(!_codesRepository.CodeExists(codeId))
                return NotFound();

            var codeToDelete = _codesRepository.GetCode(codeId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_codesRepository.DeleteCode(codeToDelete))
            {
                ModelState.AddModelError("", $"Error occured while deleting code with ID {codeId}");
                return BadRequest(ModelState);
            }

            return Ok($"Succsessfully deleted code with ID {codeId}");
        }   
    }

}
