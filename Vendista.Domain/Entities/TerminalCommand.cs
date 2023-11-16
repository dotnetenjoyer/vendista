namespace Vendista.Domain.Entities;

/// <summary>
/// Represent terminal command.
/// </summary>
public class TerminalCommand
{
    /// <summary>
    /// Terminal command ID.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Related terminal ID.
    /// </summary>
    public int TerminalId { get; set; }
    /// <summary>
    /// Command type ID.
    /// </summary>
    public int CommandId { get; set; }
    
    /// <summary>
    /// Command state.
    /// </summary>
    public string State { get; set; }
    
    /// <summary>
    /// Time of creation.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Collection of parameter values.
    /// </summary>
    public IEnumerable<int> ParameterValues { get; set; }
}