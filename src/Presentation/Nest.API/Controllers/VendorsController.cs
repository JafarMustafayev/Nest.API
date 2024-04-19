namespace Nest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorsController(IVendorService vendorService)
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

    [HttpGet("Search")]
    public async Task<ActionResult> SearchVendors([FromQuery] string query)
    {
        var res = await _vendorService.SearchVendors(query);
        return StatusCode(res.StatusCode, res);
    }
}