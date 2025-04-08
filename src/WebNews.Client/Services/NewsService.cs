using System.Net.Http.Json;
using WebNews.Client.Models;

namespace WebNews.Client.Services;

public class NewsService : INewsService
{
    private readonly HttpClient _httpClient;

    public NewsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<NewsResponse> GetLatestNewsAsync(string? nextPage = null)
    {
        var url = "api/news";
        if (!string.IsNullOrEmpty(nextPage))
        {
            url += $"?nextPage={nextPage}";
        }
        return await _httpClient.GetFromJsonAsync<NewsResponse>(url) ?? throw new InvalidOperationException("Failed to get news");
    }

    public async Task<NewsResponse> GetCachedNewsAsync()
    {
        return await _httpClient.GetFromJsonAsync<NewsResponse>("api/news/cached") ?? throw new InvalidOperationException("Failed to get cached news");
    }
}