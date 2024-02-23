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

    [HttpGet("GetAll/{page}")]
    public async Task<IActionResult> GetAll(int page = 1)
    {
        var res = await _contactService.GetAll(page, 10);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetSingle(string id)
    {
        var res = await _contactService.GetSingleAsync(id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var res = await _contactService.DeleteAsync(id);
        return StatusCode(res.StatusCode, res);
    }
}