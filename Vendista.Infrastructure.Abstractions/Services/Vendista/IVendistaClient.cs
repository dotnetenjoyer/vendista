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
    /// <param name="options">Search options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Paged list.</returns>
    Task<PagedList<TerminalCommand>> SearchTerminalCommandsAsync(TerminalCommandsSearchOptions options, CancellationToken cancellationToken);

    /// <summary>
    /// Add a new terminal command.
    /// </summary>
    /// <param name="options">Adding options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddTerminalCommandAsync(TerminalCommandAddingOptions options, CancellationToken cancellationToken);
}