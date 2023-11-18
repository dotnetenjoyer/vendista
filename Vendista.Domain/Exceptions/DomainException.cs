namespace Vendista.Domain.Exceptions;

/// <summary>
/// The exception occurs in domain part of the application.
/// It can be business or validation logic.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Optional description code for this exception.
    /// </summary>
    public string? Code { get; protected set; } = string.Empty;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DomainException() : base("An error has occured")
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public DomainException(string message) : base(message)
    {
    }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}