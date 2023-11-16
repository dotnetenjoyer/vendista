using Vendista.Domain.Entities;

namespace Vendista.Infrastructure.Abstractions.Services;

/// <summary>
/// Vendista client.
/// </summary>
public interface IVendistaClient
{
    /// <summary>
    /// Authenticates to the vendista.
    /// </summary>
    /// <param name="login">Login.</param>
    /// <param name="password">Password</param>
    Task AuthenticateAsync(string login, string password);
    
    /// <summary>
    /// Returns all command types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<IEnumerable<CommandType>> GetAllCommandTypesAsync(CancellationToken cancellationToken);
}