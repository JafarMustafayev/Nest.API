using Microsoft.AspNetCore.Authorization;
using Nest.Application.Consts;

namespace Nest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class testController : ControllerBase
{
    [HttpGet("serbest giris")]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }

    [Authorize]
    [HttpGet("Login")]
    public IActionResult Login()
    {
        return Ok("Hello World");
    }

    [Authorize(Roles = UserRoleConsts.User)]
    [HttpGet("User")]
    public IActionResult Admin()
    {
        return Ok("Hello World");
    }
}