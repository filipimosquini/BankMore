namespace Transfer.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("secure")]
    [Authorize]
    public IActionResult Secure() => Ok("ok");
}
