using Nest.Application.DTOs.Mail;

namespace Nest.API.Areas.Admin.Controllers;

[Route("api/admin/[controller]")]
[ApiController]
public class EmailManageController : ControllerBase
{
    private readonly ICustomMailService _mailService;

    public EmailManageController(ICustomMailService mailService)
    {
        _mailService = mailService;
    }

    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmail([FromForm] MailRequest request)
    {
        await _mailService.SendEmailAsync(request);

        return Ok(new ResponseDTO()
        {
            Message = "Email sent successfully",
            Success = true,
            StatusCode = 200
        });
    }

    [HttpGet("GetAllMails/{page}")]
    public async Task<IActionResult> GetAllMails(int page = 1)
    {
        var res = await _mailService.GetAllMailsAsync(page);

        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("GetMailById/{id}")]
    public async Task<IActionResult> GetMailById(string id)
    {
        var res = await _mailService.GetMailByIdAsync(id);

        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var res = await _mailService.DeleteMessage(id);

        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("RecycleBins")]
    public async Task<IActionResult> RecycleBins()
    {
        var res = await _mailService.RecycleBins();

        return StatusCode(res.StatusCode, res);
    }
}