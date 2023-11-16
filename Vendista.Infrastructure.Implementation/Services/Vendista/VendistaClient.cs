using RestSharp;
using Vendista.Domain.Entities;
using Vendista.Infrastructure.Abstractions.Services;
using Vendista.Infrastructure.Implementation.Services.Vendista.Dtos;

namespace Vendista.Infrastructure.Implementation.Services.Vendista;

/// <summary>
/// Implementation of <see cref="IVendistaClient"/>.
/// </summary>
public class VendistaClient : IVendistaClient, IDisposable
{
    private const string baseAddress = "http://178.57.218.210:398";
    
    private string authenticationToken = string.Empty;

    private readonly RestClient client;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="login">Login.</param>
    /// <param name="password">Password.</param>
    public VendistaClient()
    {
        var options = new RestClientOptions(baseAddress);
        client = new RestClient(options);
    }

    /// <inheritdoc/>
    public async Task AuthenticateAsync(string login, string password)
    {
        var request = new RestRequest($"token?login={login}&password={password}");
        var response = await client.GetAsync<AuthenticationResponse>(request);
        authenticationToken = response.Token;
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<CommandType>> GetAllCommandTypesAsync(CancellationToken cancellationToken)
    {
        ThrowIfNotAuthenticated();

        var request = new RestRequest($"/commands/types?token={authenticationToken}");
        var response = await client.GetAsync<PagedList<CommandType>>(request, cancellationToken);

        return response.Items;
    }

    private void ThrowIfNotAuthenticated()
    {
        if (string.IsNullOrEmpty(authenticationToken))
        {
            throw new Exception("Is not authenticated");
        }
    }
    
    /// <inheritdoc />
    public void Dispose()
    {
        client.Dispose();
    }
}