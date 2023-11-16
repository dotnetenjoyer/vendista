using MediatR;

namespace Vendista.UseCases.Terminals.AddTerminalCommand;

/// <summary>
/// Command to add terminal command.
/// </summary>
public class AddTerminalCommandCommand : IRequest
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