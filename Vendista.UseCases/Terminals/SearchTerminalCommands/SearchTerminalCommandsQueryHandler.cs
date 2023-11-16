using AutoMapper;
using MediatR;
using Vendista.Domain.Entities;
using Vendista.Infrastructure.Abstractions.Services;

namespace Vendista.UseCases.Terminals.SearchTerminalCommands;

/// <summary>
/// Handler for <see cref="SearchTerminalCommandsQuery"/>
/// </summary>
internal class SearchTerminalCommandsQueryHandler : IRequestHandler<SearchTerminalCommandsQuery, Common.PagedList<TerminalCommand>>
{
    private readonly IVendistaClient vendistaClient;
    private readonly IMapper mapper;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public SearchTerminalCommandsQueryHandler(IVendistaClient vendistaClient, IMapper mapper)
    {
        this.vendistaClient = vendistaClient;
        this.mapper = mapper;
    }

    /// <inheritidoc />
    public async Task<Common.PagedList<TerminalCommand>> Handle(SearchTerminalCommandsQuery query, CancellationToken cancellationToken)
    {
        await vendistaClient.InitAsync(cancellationToken);

        var searchOptions = mapper.Map<TerminalCommandsSearchOptions>(query);
        var commands = await vendistaClient.SearchTerminalCommandsAsync(searchOptions, cancellationToken);
        
        return mapper.Map<Common.PagedList<TerminalCommand>>(commands);
    }
}