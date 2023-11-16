namespace Vendista.Infrastructure.Abstractions.Services;

/// <summary>
/// Contains members to search terminal commands.
/// </summary>
public class TerminalCommandsSearchOptions
{
    /// <summary>
    /// Page.
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Terminal ID.
    /// </summary>
    public int TerminalId { get; set; }
    
    /// <summary>
    /// Order by column name.
    /// </summary>
    public string OrderBy { get; set; }
    
    /// <summary>
    /// Indicates whether the order is descending or not.
    /// </summary>
    public bool OrderDesc { get; set; }
}