using Microsoft.AspNetCore.Mvc;
using Nest.Application.Abstractions.Services;
using Nest.Application.DTOs.Contact;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contacts = await _contactService.GetAll();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(string id)
    {
        var contact = await _contactService.GetSingleAsync(id);
        return Ok(contact);
    }

    [HttpPost("PostContact")]
    public async Task<IActionResult> Create([FromForm] ContactCreateDTO contactCreateDTO)
    {
        var response = await _contactService.CreateAsync(contactCreateDTO);
        return StatusCode(response.StatusCode, response);
    }
}