using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Vendista.Domain.Exceptions;

namespace Vendista.WebApp;

/// <summary>
/// API exception middleware.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly IWebHostEnvironment environment;

    private static string ErrorsKey = "errors";
    private static string CodeKey = "code";
    private const string ProblemMimeType = "application/problem+json";
    
    private static readonly IDictionary<Type, int> ExceptionStatusCodes = new Dictionary<Type, int>
    {
        [typeof(NotImplementedException)] = StatusCodes.Status501NotImplemented,
        [typeof(DomainException)] = StatusCodes.Status400BadRequest
    };
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
    {
        this.next = next;
        this.logger = logger;
        this.environment = environment;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">Http context.</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            if (context.Response.HasStarted)
            {
                logger.LogWarning("The response has already started, " +
                                  "the API exception middleware will not be executed.");
                throw;
            }
            
            var problemDetails = ProcessException(exception);
            problemDetails.Instance = context.Request.Path;
            context.Response.Clear();
            context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            context.Response.ContentType = ProblemMimeType;
            
            var response = JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });
            
            context.Response.WriteAsync(response);
        }
    }

    private ProblemDetails ProcessException(Exception exception)
    {
        var problemDetails = new ProblemDetails();

        switch (exception)
        {
            case DomainException domainException:
                AddExceptionInfoToProblemDetails(problemDetails, domainException);
                break;
            
            default:
                problemDetails.Title = "Internal server error.";
                problemDetails.Status = GetExceptionStatusCode(exception);
                
                if (!environment.IsProduction())
                {
                    problemDetails.Extensions["debug_exception"] = new Dictionary<string, string>
                    {
                        ["Type"] = exception.GetType().ToString(),
                        ["Message"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace
                    };
                }

                logger.LogError(exception, exception.Message);
                break;
        }
        
        return problemDetails;
    }

    private void AddExceptionInfoToProblemDetails(ProblemDetails problemDetails, DomainException exception)
    {
        problemDetails.Title = exception.Message;
        problemDetails.Type = exception.GetType().Name;
        problemDetails.Status = GetExceptionStatusCode(exception);
        AddCodeToProblemDetails(problemDetails, exception.Code);
    }

    private void AddCodeToProblemDetails(ProblemDetails problemDetails, string? code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            problemDetails.Extensions[CodeKey] = code;
        }
    }
    
    private int GetExceptionStatusCode(Exception exception)
    {
        if (ExceptionStatusCodes.ContainsKey(exception.GetType()))
        {
            return ExceptionStatusCodes[exception.GetType()];
        }
        
        return StatusCodes.Status500InternalServerError;
    }
}