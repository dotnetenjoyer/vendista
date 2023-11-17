using AutoMapper;
using Vendista.Infrastructure.Abstractions.Services;
using Vendista.UseCases.Terminals.SendTerminalCommand;
using Vendista.UseCases.Terminals.SearchTerminalCommands;

namespace Vendista.UseCases.Terminals;

/// <summary>
/// Contains mapping configuration for terminals.
/// </summary>
public class TerminalProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TerminalProfile()
    {
        CreateMap<SearchTerminalCommandsQuery, TerminalCommandsSearchOptions>();
        CreateMap<SendTerminalCommandCommand, TerminalCommandSendingOptions>();
    }
}