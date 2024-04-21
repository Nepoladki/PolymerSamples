using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PolymerSamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : Controller
    {
        private readonly ISamplesRepository _samplesRepository;
        public SamplesController(ISamplesRepository samplesRepository)
        {
            _samplesRepository = samplesRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Code>)]
        public IActionResult GetCodes()
        {
            var codes = _samplesRepository.GetCodes();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(codes);
        }
    }
}
