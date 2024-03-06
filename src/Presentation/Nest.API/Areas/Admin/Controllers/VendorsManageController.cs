namespace Nest.API.Areas.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsManageController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorsManageController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpGet("GetAll/{page}")]
    public async Task<ActionResult> GetAll(int page = 1)
    {
        var res = await _vendorService.GetAllVendorsAsync(page);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("Get/{Id}")]
    public async Task<ActionResult> GetSingleVendor(string Id)
    {
        var res = await _vendorService.GetVendorByIdAsync(Id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromForm] VendorCreateDTO vendorCreateDTO)
    {
        var res = await _vendorService.CreateVendorAsync(vendorCreateDTO);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromForm] VendorUpdateDTO vendorUpdateDTO)
    {
        var res = await _vendorService.UpdateVendorAsync(vendorUpdateDTO);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("Delete/{Id}")]
    public async Task<ActionResult> Delete(string Id)
    {
        var res = await _vendorService.DeleteVendorAsync(Id);
        return StatusCode(res.StatusCode, res);
    }
}