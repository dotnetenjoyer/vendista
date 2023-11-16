using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vendista.Domain.Entities;
using Vendista.UseCases.Common;

namespace Vendista.UseCases.Terminals.SearchTerminalCommands;

/// <summary>
/// Search for terminal commands.
/// </summary>
public class SearchTerminalCommandsQuery : IRequest<PagedList<TerminalCommand>>
{
    /// <summary>
    /// Page.
    /// </summary>
    [FromQuery]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Page size.
    /// </summary>
    [FromQuery]
    public int PageSize { get; set; } = 100;

    /// <summary>
    /// Terminal ID.
    /// </summary>
    [FromRoute]
    public int TerminalId { get; set; }
    
    /// <summary>
    /// Order by column index.
    /// </summary>
    [FromQuery]
    public int OrderBy { get; set; }

    /// <summary>
    /// Indicates whether the order is descending or not.
    /// </summary>
    [FromQuery]
    public bool OrderDesc { get; set; } = true;
}