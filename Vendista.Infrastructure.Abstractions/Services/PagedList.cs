namespace Vendista.Infrastructure.Abstractions.Services;

/// <summary>
/// Represent of query results with pagination support.
/// </summary>
/// <typeparam name="T">Type of query.</typeparam>
public class PagedList<T>
{
    /// <summary>
    /// Number of current page.
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Page size. 
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total items.
    /// </summary>
    public int TotalItems { get; set; }
    
    /// <summary>
    /// Collection of searched items.
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}