namespace Nest.API.Areas.Admin.Controllers;

[Route("api/admin/[controller]")]
[ApiController]
public class ContactManageController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactManageController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _contactService.GetAll();
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetSingle(string id)
    {
        var res = await _contactService.GetSingleAsync(id);
        return StatusCode(res.StatusCode, res);
    }
}