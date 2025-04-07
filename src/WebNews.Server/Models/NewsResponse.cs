using System.Text.Json.Serialization;

namespace WebNews.Server.Models;

public class NewsResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("totalResults")]
    public int TotalResults { get; set; }

    [JsonPropertyName("results")]
    public List<NewsArticle> Results { get; set; } = new();

    [JsonPropertyName("nextPage")]
    public string? NextPage { get; set; }
}