using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PolymerSamples.Controllers;

[ApiController]
[Route("api/error/")]
public class ErrorController : ControllerBase
{
    [HttpDelete]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: exception?.Message);
    }
}

