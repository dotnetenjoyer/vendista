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

    private readonly string login;
    private readonly string password;
    private readonly RestClient client;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="login">Login.</param>
    /// <param name="password">Password.</param>
    public VendistaClient(string login, string password)
    {
        this.login = login;
        this.password = password;
        
        var options = new RestClientOptions(baseAddress);
        client = new RestClient(options);
    }

    /// <inheritdoc/>
    public async Task InitAsync(CancellationToken cancellationToken)
    {
        var request = new RestRequest($"token?login={login}&password={password}");
        var response = await client.GetAsync<AuthenticationResponse>(request, cancellationToken);
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

    /// <inheritdoc/>
    public async Task<PagedList<TerminalCommand>> SearchTerminalCommandsAsync(TerminalCommandsSearchOptions searchOptions, CancellationToken cancellationToken)
    {
        ThrowIfNotAuthenticated();

        var request = new RestRequest($"/terminals/{searchOptions.TerminalId}/commands")
            .AddQueryParameter("token", authenticationToken)
            .AddQueryParameter("pageNumber", searchOptions.Page)
            .AddQueryParameter("itemsOnPage", searchOptions.PageSize);
        
        var response = await client.GetAsync(request, cancellationToken);
        
        var pagedList = JObject.Parse(response.Content);
        var result = new PagedList<TerminalCommand>
        {
            Page = pagedList.Value<int>("page_number"),
            PageSize = pagedList.Value<int>("items_per_page"),
            TotalItems = pagedList.Value<int>("items_count"),
            Items = pagedList["items"].Select(ParseTerminalCommand)
        };
        
        return result;
    }

    private TerminalCommand ParseTerminalCommand(JToken terminalCommand)
    {
        var command = new TerminalCommand
        {
            Id = terminalCommand.Value<int>("id"),
            TerminalId = terminalCommand.Value<int>("terminal_id"),
            CommandId = terminalCommand.Value<int>("command_id"),
            State = terminalCommand.Value<string>("state_name"),
            CreatedAt = terminalCommand.Value<DateTime>("time_created"),
            ParameterValues = ParseTerminalCommandParameterValues(terminalCommand)
        };

        return command;
    }

    private IEnumerable<int> ParseTerminalCommandParameterValues(JToken terminalCommand)
    {
        var parameterValues = new List<int>();
        
        for (int i = 1; i < int.MaxValue; i++)
        {
            var parameterValue = terminalCommand.Value<int?>($"parameter{i}");
            if (!parameterValue.HasValue)
            {
                break;
            }
                
            parameterValues.Add(parameterValue.Value);
        }

        return parameterValues;
    }

    private CommandType ParseCommandType(JToken commandType)
    {
        return new CommandType
        {
            Id = commandType.Value<int>("id"),
            Name = commandType.Value<string>("name"),
            Parameters = ParseCommandTypeParameters(commandType)
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