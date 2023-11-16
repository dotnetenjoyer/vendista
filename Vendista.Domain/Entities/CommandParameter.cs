namespace Vendista.Domain.Entities;

/// <summary>
/// Represent terminal command parameter.
/// </summary>
public class CommandParameter
{
    /// <summary>
    /// Parameter name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Parameter default value.
    /// </summary>
    public object DefaultValue { get; set; }
}