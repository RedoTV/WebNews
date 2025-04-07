using WebNews.Server.Models;

namespace WebNews.Server.Services;

public interface INewsService
{
    Task<NewsResponse> GetLatestNewsAsync(string? nextPage = null);
    Task<NewsResponse> GetCachedNewsAsync();
}