using AutoMapper;
using MediatR;
using Vendista.Infrastructure.Abstractions.Services;

namespace Vendista.UseCases.Terminals.AddTerminalCommand;

/// <summary>
/// Handler for <see cref="AddTerminalCommandCommand"/>.
/// </summary>
internal class AddTerminalCommandCommandHandler : IRequestHandler<AddTerminalCommandCommand>
{
    private readonly IVendistaClient vendistaClient;
    private readonly IMapper mapper;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public AddTerminalCommandCommandHandler(IVendistaClient vendistaClient, IMapper mapper)
    {
        this.vendistaClient = vendistaClient;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task Handle(AddTerminalCommandCommand command, CancellationToken cancellationToken)
    {
        await vendistaClient.InitAsync(cancellationToken);
        var options = mapper.Map<TerminalCommandAddingOptions>(command);
        await vendistaClient.AddTerminalCommandAsync(options, cancellationToken);
    }
}