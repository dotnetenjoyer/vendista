using MediatR;
using Vendista.Domain.Entities;
using Vendista.Infrastructure.Abstractions.Services;

namespace Vendista.UseCases.Terminals.SearchCommands;

/// <summary>
/// Handler for <see cref="SearchCommandsQuery"/>.
/// </summary>
internal class SearchCommandsQueryHandler : IRequestHandler<SearchCommandsQuery, IEnumerable<CommandType>>
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
    public async Task<IEnumerable<CommandType>> Handle(SearchCommandsQuery request, CancellationToken cancellationToken)
    {
        await client.InitAsync(cancellationToken);
        var commandTypes = await client.GetAllCommandTypesAsync(cancellationToken);
        return commandTypes;
    }
}