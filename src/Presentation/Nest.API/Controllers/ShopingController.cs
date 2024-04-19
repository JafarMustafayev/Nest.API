namespace Nest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopingController : ControllerBase
{
    private readonly IShopingService _shopingService;

    public ShopingController(IShopingService shopingService)
    {
        _shopingService = shopingService;
    }

    [HttpPost("LikeDislike")]
    public async Task<IActionResult> LikeDislike([FromForm] LikeAndUnLikeDTO likeDislIkeDTO)
    {
        var response = await _shopingService.LikeDislike(likeDislIkeDTO);
        return Ok(response);
    }

    [HttpGet("GetLikeDislikeByUser")]
    public async Task<IActionResult> GetLikeDislikeByUser([FromQuery] string refreshtoken, [FromQuery] int page = 1)
    {
        var response = await _shopingService.GetLikeDislikeByUser(refreshtoken, page);
        return Ok(response);
    }
}