namespace Vendista.Domain.Entities;

/// <summary>
/// Type of terminal command.
/// </summary>
public class CommandType
{
    /// <summary>
    /// Command id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Command type name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Collection of command parameters.
    /// </summary>
    public IEnumerable<CommandParameter> Parameters { get; set; }
}