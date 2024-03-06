namespace Nest.API.Areas.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsManageController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsManageController(IProductService productService)
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

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromForm] ProductCreateDTO productCreateDTO)
    {
        var res = await _productService.CreateProductAsync(productCreateDTO);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromForm] ProductUpdateDTO productUpdateDTO)
    {
        var res = await _productService.UpdateProductAsync(productUpdateDTO);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("Delete/{Id}")]
    public async Task<ActionResult> Delete(string Id)
    {
        var res = await _productService.DeleteProductAsync(Id);
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("DeleteImage/{productId}/{imageId}")]
    public async Task<ActionResult> DeleteImage(string productId, string imageId)
    {
        var res = await _productService.DeleteProductImageAsync(productId, imageId);
        return StatusCode(res.StatusCode, res);
    }
}