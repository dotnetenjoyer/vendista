using Newtonsoft.Json.Linq;
using RestSharp;
using Vendista.Domain.Entities;
using Vendista.Infrastructure.Abstractions.Services;
using Vendista.Infrastructure.Implementation.Services.Vendista.Dtos;

namespace Vendista.Infrastructure.Implementation.Services.Vendista;

/// <summary>
/// Implementation of <see cref="IVendistaClient"/>.
/// </summary>
public class VendistaClient : IVendistaClient, IDisposable
{
    private const string baseAddress = "http://178.57.218.210:398";
    
    private string authenticationToken = string.Empty;

    private readonly RestClient client;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="login">Login.</param>
    /// <param name="password">Password.</param>
    public VendistaClient()
    {
        var options = new RestClientOptions(baseAddress);
        client = new RestClient(options);
    }

    /// <inheritdoc/>
    public async Task AuthenticateAsync(string login, string password)
    {
        var request = new RestRequest($"token?login={login}&password={password}");
        var response = await client.GetAsync<AuthenticationResponse>(request);
        authenticationToken = response.Token;
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<CommandType>> GetAllCommandTypesAsync(CancellationToken cancellationToken)
    {
        ThrowIfNotAuthenticated();

        var request = new RestRequest($"/commands/types?token={authenticationToken}");
        var response = await client.GetAsync(request, cancellationToken);

        var pagedList = JObject.Parse(response.Content);
        var commands = pagedList["items"].Select(ParseCommandType);
        return commands;
    }

    private CommandType ParseCommandType(JToken commandType)
    {
        return new CommandType
        {
            Id = commandType.Value<int>("id"),
            Name = commandType.Value<string>("name"),
            Parameters = ParseCommandTypeParameters(commandType),
        };
    }
    
    private IEnumerable<CommandParameter> ParseCommandTypeParameters(JToken commandType)
    {
        var parameters = new List<CommandParameter>();

        for (int i = 1; i < int.MaxValue; i++)
        {
            var parameterName = commandType.Value<string>($"parameter_name{i}");
            if (string.IsNullOrEmpty(parameterName))
            {
                break;
            }
                
            var parameter = new CommandParameter
            {
                Name = parameterName,
                DefaultValue = commandType.Value<string>($"parameter_default_value{i}")
            };

            parameters.Add(parameter);
        }

        return parameters;
    }
    
    private void ThrowIfNotAuthenticated()
    {
        if (string.IsNullOrEmpty(authenticationToken))
        {
            throw new Exception("Is not authenticated");
        }
    }
    
    /// <inheritdoc />
    public void Dispose()
    {
        client.Dispose();
    }
}