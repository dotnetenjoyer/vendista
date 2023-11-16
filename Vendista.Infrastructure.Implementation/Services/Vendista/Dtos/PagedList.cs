using System.Text.Json.Serialization;

namespace Vendista.Infrastructure.Implementation.Services.Vendista.Dtos;

/// <summary>
/// Represent of query results with pagination support..
/// </summary>
/// <typeparam name="T">Type of query.</typeparam>
public class PagedList<T>
{
    /// <summary>
    /// Number of current page.
    /// </summary>
    [JsonPropertyName("page_number")]
    public int Page { get; set; }
    
    /// <summary>
    /// Page size. 
    /// </summary>
    [JsonPropertyName("items_per_page")]
    public int PageSize { get; set; }

    /// <summary>
    /// Total items.
    /// </summary>
    [JsonPropertyName("items_count")]
    public int TotalItems { get; set; }
    
    /// <summary>
    /// Collection of searched items.
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}