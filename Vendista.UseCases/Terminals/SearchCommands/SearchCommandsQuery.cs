using MediatR;
using Vendista.Domain.Entities;

namespace Vendista.UseCases.Terminals.SearchCommands;

/// <summary>
/// Search for terminal command types.
/// </summary>
public class SearchCommandsQuery : IRequest<IEnumerable<CommandType>>
{
    
}