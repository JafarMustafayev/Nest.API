namespace Nest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost("PostContact")]
    public async Task<IActionResult> Create([FromForm] ContactCreateDTO contactCreateDTO)
    {
        var response = await _contactService.CreateAsync(contactCreateDTO);
        return StatusCode(response.StatusCode, response);
    }
}