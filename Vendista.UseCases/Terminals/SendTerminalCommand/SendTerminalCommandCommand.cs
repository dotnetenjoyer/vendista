using MediatR;

namespace Vendista.UseCases.Terminals.SendTerminalCommand;

/// <summary>
/// Command to send terminal command.
/// </summary>
public class SendTerminalCommandCommand : IRequest
{
    /// <summary>
    /// Terminal ID.
    /// </summary>
    public int TerminalId { get; set; }
    
    /// <summary>
    /// Command type ID.
    /// </summary>
    public int CommandTypeId { get; set; }
    
    /// <summary>
    /// Collection of command parameter values.
    /// </summary>
    public IEnumerable<int> CommandValues { get; set; }
}
