using Microsoft.AspNetCore.Mvc;
using MediatR;
using Vendista.Domain.Entities;
using Vendista.UseCases.Common;
using Vendista.UseCases.Terminals.AddTerminalCommand;
using Vendista.UseCases.Terminals.SearchTerminalCommands;

namespace Vendista.WebApp.Controllers;

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
    /// Add a new command to terminal.
    /// </summary>
    [HttpPost("command")]
    public Task AddTerminalCommandAsync([FromBody] AddTerminalCommandCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);
}