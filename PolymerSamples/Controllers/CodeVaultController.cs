﻿using Microsoft.AspNetCore.Mvc;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/codesinvaults/[Controller]")]
    [ApiController]
    public class CodeVaultController : ControllerBase
    {
        private readonly ICodeVaultRepository _codeVaultRepository;
        public CodeVaultController(ICodeVaultRepository codeVaultRepository)
        {
            _codeVaultRepository = codeVaultRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CodesVaults>))]
        public IActionResult GetAllCodeVaults()
        {
            var codeVaults = _codeVaultRepository.GetAllCodeVaults().Select(c => c.AsDTO());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codeVaults);
        }

        [HttpGet("{codeVaultId}")]
        [ProducesResponseType(200, Type = typeof(CodesVaults))]
        [ProducesResponseType(400)]
        public IActionResult GetCodeVault(Guid codeVaultId)
        {
            if (!_codeVaultRepository.CodeVaultExists(codeVaultId))
                return NotFound();

            var codeVault = _codeVaultRepository.GetCodeVaultById(codeVaultId).AsDTO();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codeVault);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCodeVault(CodeVaultDTO newCodeVault)
        {
            if (newCodeVault is null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var codeVault = DTOToModel.FromDTO(newCodeVault);

            if (!_codeVaultRepository.CreateCodeVault(codeVault))
            {
                ModelState.AddModelError("", "Saving Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created and saved new code and vault relation");
        }

        [HttpDelete("{codeVaultId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCodeVault(Guid codeVaultId)
        {
            if (!_codeVaultRepository.CodeVaultExists(codeVaultId))
                return BadRequest(ModelState);

            var codeVaultToDelete = _codeVaultRepository.GetCodeVaultById(codeVaultId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_codeVaultRepository.DeleteCodeVault(codeVaultToDelete))
            {
                ModelState.AddModelError("", $"Error occured while deleting code with ID {codeVaultId}");
                return StatusCode(500, ModelState);
            }

            return Ok($"Succsessfully deleted code with ID {codeVaultId}");
        }
    }
}
