using WebNews.Client.Models;

namespace WebNews.Client.Services;

public interface INewsService
{
    Task<NewsResponse> GetLatestNewsAsync(string? nextPage = null);
    Task<NewsResponse> GetCachedNewsAsync();
}
