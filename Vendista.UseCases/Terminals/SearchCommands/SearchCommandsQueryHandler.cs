using MediatR;
using Vendista.Infrastructure.Abstractions.Services;

namespace Vendista.UseCases.Terminals.SearchCommands;

/// <summary>
/// Handler for <see cref="SearchCommandsQuery"/>.
/// </summary>
internal class SearchCommandsQueryHandler : IRequestHandler<SearchCommandsQuery>
{
    private readonly IVendistaClient client;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="client"></param>
    public SearchCommandsQueryHandler(IVendistaClient client)
    {
        this.client = client;
    }

    /// <inheritdoc />
    public async Task Handle(SearchCommandsQuery request, CancellationToken cancellationToken)
    {
        await client.AuthenticateAsync("user2", "password2");
        var commandTypes = await client.GetAllCommandTypesAsync(cancellationToken);

        // throw new NotImplementedException();
    }
}