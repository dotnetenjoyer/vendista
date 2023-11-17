namespace Vendista.Infrastructure.Abstractions.Services;

/// <summary>
/// Contains elements for adding a command to the terminal.
/// </summary>
public class TerminalCommandSendingOptions
{
    /// <summary>
    /// Terminal id.
    /// </summary>
    public int TerminalId { get; set; }
    
    /// <summary>
    /// Command type id.
    /// </summary>
    public int CommandTypeId { get; set; }
    
    /// <summary>
    /// Collection of command values.
    /// </summary>
    public IEnumerable<int> CommandValues { get; set; }
}