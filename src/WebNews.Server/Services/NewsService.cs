using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WebNews.Server.Models;

namespace WebNews.Server.Services;

public class NewsService : INewsService
{
    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _cache;
    private readonly IConfiguration _configuration;
    private const string CacheKey = "latest_news";
    private const int CacheExpirationMinutes = 10;

    public NewsService(
        HttpClient httpClient,
        IDistributedCache cache,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _cache = cache;
        _configuration = configuration;
    }

    public async Task<NewsResponse> GetLatestNewsAsync(string? nextPage = null)
    {
        var apiKey = _configuration["ApiKey"];
        var baseUrl = "https://newsdata.io/api/1/latest";
        var url = $"{baseUrl}?country=by&category=top&apikey={apiKey}&language=ru";

        if (!string.IsNullOrEmpty(nextPage))
        {
            url += $"&page={nextPage}";
        }

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<NewsResponse>(content) ?? throw new InvalidOperationException("Failed to deserialize news response");
    }

    public async Task<NewsResponse> GetCachedNewsAsync()
    {
        var cachedNews = await _cache.GetStringAsync(CacheKey);
        if (!string.IsNullOrEmpty(cachedNews))
        {
            return JsonSerializer.Deserialize<NewsResponse>(cachedNews) ?? throw new InvalidOperationException("Failed to deserialize cached news");
        }

        var news = await GetLatestNewsAsync();
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheExpirationMinutes)
        };

        await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(news), options);
        return news;
    }
}