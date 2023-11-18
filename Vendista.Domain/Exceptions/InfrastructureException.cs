namespace Vendista.Domain.Exceptions;

/// <summary>
/// The exception occurs when something wrong happens on the server side. When a resource is not available
/// or works incorrectly so that system cannot process requests. The client must provide source exception.
/// It can be mapped to 500 HTTP status code.
/// </summary>
public class InfrastructureException : DomainException
{
    ///<summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public InfrastructureException(string message) : base(message)
    {
    }
    
    ///<summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="innerException">Inner exception.</param>
    public InfrastructureException(string message, Exception innerException) : base(message, innerException)
    {
    }
}