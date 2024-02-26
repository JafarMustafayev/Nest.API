namespace Nest.API.Areas.Admin.Controllers;

[Route("api/admin/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ICustomMailService _mailService;

    public EmailController(ICustomMailService mailService)
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

    [HttpGet("SendEmailForContact")]
    public async Task<IActionResult> SendEmailForContact()
    {
        var res = new ResponseDTO()
        {
            Message = "Email sent successfully",
            Success = true,
            StatusCode = 200,
            Payload = await _mailService.GetAllMailsAsync()
        };

        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("GetMailById")]
    public async Task<IActionResult> GetMailById(string id)
    {
        var res = new ResponseDTO()
        {
            Message = "Email sent successfully",
            Success = true,
            StatusCode = 200,
            Payload = await _mailService.GetMailByIdAsync(id)
        };

        return StatusCode(res.StatusCode, res);
    }
}