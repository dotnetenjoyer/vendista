using Vendista.Domain.Entities;

namespace Vendista.Infrastructure.Abstractions.Services;

/// <summary>
/// Vendista client.
/// </summary>
public interface IVendistaClient : IDisposable
{
    /// <summary>
    /// Initialize vendista client.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task InitAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Returns all command types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<IEnumerable<CommandType>> GetAllCommandTypesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Search for terminal commands.
    /// </summary>
    /// <param name="searchOptions">Search options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<PagedList<TerminalCommand>> SearchTerminalCommandsAsync(TerminalCommandsSearchOptions searchOptions, CancellationToken cancellationToken);
}