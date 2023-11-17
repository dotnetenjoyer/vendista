using Microsoft.AspNetCore.Mvc;
using MediatR;
using Vendista.Domain.Entities;
using Vendista.UseCases.Common;
using Vendista.UseCases.Terminals.SendTerminalCommand;
using Vendista.UseCases.Terminals.SearchTerminalCommands;

namespace Vendista.WebApp.Controllers;

/// <summary>
/// Contains methods to manage terminals.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TerminalController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public TerminalController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Search for terminal commands.
    /// </summary>
    [HttpGet("{TerminalId}/commands")]
    public Task<PagedList<TerminalCommand>> SearchTerminalCommandsAsync([FromQuery]SearchTerminalCommandsQuery query, CancellationToken cancellationToken)
        => mediator.Send(query, cancellationToken);

    /// <summary>
    /// Send a command to terminal.
    /// </summary>
    [HttpPost("command")]
    public Task SendTerminalCommandAsync([FromBody] SendTerminalCommandCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);
}