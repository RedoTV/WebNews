using System.Text.Json.Serialization;

namespace WebNews.Server.Models;

public class NewsArticle
{
    [JsonPropertyName("article_id")]
    public string ArticleId { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("link")]
    public string Link { get; set; } = string.Empty;

    [JsonPropertyName("keywords")]
    public List<string> Keywords { get; set; } = new();

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("pubDate")]
    public string PubDate { get; set; } = string.Empty;

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("source_name")]
    public string SourceName { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public List<string> Country { get; set; } = new();

    [JsonPropertyName("category")]
    public List<string> Category { get; set; } = new();
}