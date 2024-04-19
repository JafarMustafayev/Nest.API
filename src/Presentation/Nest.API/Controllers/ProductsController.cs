namespace Nest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll/{page}")]
    public async Task<ActionResult> GetAll(int page = 1)
    {
        var res = await _productService.GetAllProductsAsync(page);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("Get/{Id}")]
    public async Task<ActionResult> GetSingleProduct(string Id)
    {
        var res = await _productService.GetProductByIdAsync(Id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("Search")]
    public async Task<ActionResult> SearchProducts([FromQuery] string query)
    {
        var res = await _productService.SearchProducts(query);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("SearchVendorProducts/{vendorId}")]
    public async Task<ActionResult> SearchVendorProducts(string vendorId, [FromQuery] string query)
    {
        var res = await _productService.SearchVendorProducts(query, vendorId);
        return StatusCode(res.StatusCode, res);
    }
}