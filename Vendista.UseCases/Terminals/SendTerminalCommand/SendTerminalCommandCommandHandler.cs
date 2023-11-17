using AutoMapper;
using MediatR;
using Vendista.Infrastructure.Abstractions.Services;

namespace Vendista.UseCases.Terminals.SendTerminalCommand;

/// <summary>
/// Handler for <see cref="SendTerminalCommandCommand"/>.
/// </summary>
internal class SendTerminalCommandCommandHandler : IRequestHandler<SendTerminalCommandCommand>
{
    private readonly IVendistaClient vendistaClient;
    private readonly IMapper mapper;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public SendTerminalCommandCommandHandler(IVendistaClient vendistaClient, IMapper mapper)
    {
        this.vendistaClient = vendistaClient;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task Handle(SendTerminalCommandCommand command, CancellationToken cancellationToken)
    {
        await vendistaClient.InitAsync(cancellationToken);
        var options = mapper.Map<TerminalCommandSendingOptions>(command);
        await vendistaClient.SendTerminalCommandAsync(options, cancellationToken);
    }
}