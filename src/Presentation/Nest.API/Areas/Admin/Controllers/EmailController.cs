﻿namespace Nest.API.Areas.Admin.Controllers;

[Route("api/admin/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IMailService _mailService;

    public EmailController(IMailService mailService)
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
}