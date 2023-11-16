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

// /// <summary>
// /// Type of terminal command.
// /// </summary>
// public class CommandType
// {
//     /// <summary>
//     /// Command id.
//     /// </summary>
//     public int Id { get; set; }
//     
//     /// <summary>
//     /// Command type name.
//     /// </summary>
//     public string Name { get; set; }
//
//     /// <summary>
//     /// Collection of command parameters.
//     /// </summary>
//     public IEnumerable<CommandParameter> Parameters { get; set; }
// }

// /// <summary>
// /// Represent terminal command parameter.
// /// </summary>
// public class CommandParameter
// {
//     /// <summary>
//     /// Parameter name.
//     /// </summary>
//     public string Name { get; set; }
//     
//     /// <summary>
//     /// Parameter default value.
//     /// </summary>
//     public object DefaultValue { get; set; }
// }