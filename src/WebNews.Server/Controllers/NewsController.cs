using Microsoft.AspNetCore.Mvc;
using WebNews.Server.Services;

namespace WebNews.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLatestNews([FromQuery] string? nextPage = null)
    {
        var news = await _newsService.GetLatestNewsAsync(nextPage);
        return Ok(news);
    }

    [HttpGet("cached")]
    public async Task<IActionResult> GetCachedNews()
    {
        var news = await _newsService.GetCachedNewsAsync();
        return Ok(news);
    }
}