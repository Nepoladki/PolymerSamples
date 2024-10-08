﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.Authorization;
using PolymerSamples.DTO;
using PolymerSamples.Interfaces;
using PolymerSamples.Migrations;
using PolymerSamples.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/codes/")]
    [ApiController]
    [Authorize]
    public class CodeController : ControllerBase
    {
        private readonly ICodeRepository _codesRepository;
        private readonly ICodeValidator _codeValidator;
        public CodeController(ICodeRepository codesRepository, ICodeValidator codeValidator)
        {
            _codesRepository = codesRepository;
            _codeValidator = codeValidator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CodeIncludesVaultsDTO>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllCodesAsync()
        {
            var codes = await _codesRepository.GetAllCodesIncludingVaultsAsync();

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

            return Ok(code.AsDTO());
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCodeAsync([FromBody] CodeDTO newCode) 
        {
            if (newCode is null)
                return BadRequest("Invalid code object");

            if (!_codeValidator.ValidateCode(newCode))
                return BadRequest("Invalid ShortCodeName");

            var existingCode = await _codesRepository.GetCodeByNameAsync(newCode.code_name);

            if (existingCode is not null)
                return StatusCode(422, $"Code with this name already exists, its id is {existingCode.Id}");

            var code = DTOToModelExtensions.FromDTO(newCode);
            
            if (!await _codesRepository.CreateCodeAsync(code))
                return StatusCode(500, "Error occured while saving data to database");

            return Ok("Succesfully created and saved new code");
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpDelete("{codeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCode(Guid codeId)
        {
            if(!await _codesRepository.CodeExistsAsync(codeId))
                return NotFound();

            var codeToDelete = await _codesRepository.GetCodeByIdAsync(codeId);

            if (!await _codesRepository.DeleteCodeAsync(codeToDelete))
                return BadRequest($"Error occured while deleting code with ID {codeId}");

            return Ok($"Succsessfully deleted code with ID {codeId}");
        }

        [Authorize(Policy = AuthData.EditorPolicyName)]
        [HttpPut("{codeId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCodeAsync(Guid codeId, [FromBody] CodeDTO codeDto)
        {
            if (codeDto is null)
                return BadRequest("Invalid code object");

            if (!_codeValidator.ValidateCode(codeDto))
                return BadRequest("Invalid ShortCodeName");

            if (!await _codesRepository.CodeExistsAsync(codeId))
                return NotFound($"Did not found code with id {codeId}");

            var codeToUpdate = await _codesRepository.GetCodeByIdAsync(codeId);

            codeToUpdate.ShortCodeName = codeDto.short_code_name;
            codeToUpdate.CodeName = codeDto.code_name;
            codeToUpdate.SupplierCodeName = codeDto.supplier_code_name;
            codeToUpdate.StockLevel = codeDto.stock_level;
            codeToUpdate.Note = codeDto.note;
            codeToUpdate.TypeId = codeDto.type_id;
            codeToUpdate.Layers = codeDto.layers;
            codeToUpdate.Thickness = codeDto.thickness;

            if (!await _codesRepository.UpdateCodeAsync(codeToUpdate))
                return BadRequest($"Error occured while updating code with ID {codeId}");
            
            return Ok($"Succsessfully updated code with ID {codeId}");
        }
    }

}
