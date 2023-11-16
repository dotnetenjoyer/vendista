using MediatR;

namespace Vendista.UseCases.Terminals.AddCommand;

/// <summary>
/// Command to add command to the terminal
/// </summary>
public class AddCommandCommand : IRequest
{
    /// <summary>
    /// Terminal ID.
    /// </summary>
    public int TerminalId { get; set; }
    
    /// <summary>
    /// Command type ID.
    /// </summary>
    public int CommandId { get; set; }
    
    /// <summary>
    /// Collection of command parameter values.
    /// </summary>
    public IEnumerable<string> ParameterValues { get; set; }
}