using Newtonsoft.Json.Linq;
using RestSharp;
using Vendista.Domain.Entities;
using Vendista.Domain.Exceptions;
using Vendista.Infrastructure.Abstractions.Services;
using Vendista.Infrastructure.Implementation.Services.Vendista.Dtos;

namespace Vendista.Infrastructure.Implementation.Services.Vendista;

/// <summary>
/// Implementation of <see cref="IVendistaClient"/>.
/// </summary>
public class VendistaClient : IVendistaClient
{
    private const string baseAddress = "http://178.57.218.210:398";
    
    private string accessToken = string.Empty;

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
        
        client = new RestClient(new RestClientOptions(baseAddress));
    }

    /// <inheritdoc/>
    public async Task InitAsync(CancellationToken cancellationToken)
    {
        var request = new RestRequest($"token?login={login}&password={password}");
        var response = await client.GetAsync<AuthenticationResponse>(request, cancellationToken);
        accessToken = response.Token;
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<CommandType>> GetAllCommandTypesAsync(CancellationToken cancellationToken)
    {
        ThrowIfNotAuthenticated();
        
        var request = new RestRequest($"/commands/types?token={accessToken}");
        var response = await client.GetAsync(request, cancellationToken);
        
        var pagedList = JObject.Parse(response.Content);
        var commands = pagedList["items"].Select(ParseCommandType);
        return commands;
    }

    /// <inheritdoc/>
    public async Task<PagedList<TerminalCommand>> SearchTerminalCommandsAsync(TerminalCommandsSearchOptions options, CancellationToken cancellationToken)
    {
        ThrowIfNotAuthenticated();

        var request = new RestRequest($"/terminals/{options.TerminalId}/commands")
            .AddQueryParameter("token", accessToken)
            .AddQueryParameter("pageNumber", options.Page)
            .AddQueryParameter("itemsOnPage", options.PageSize)
            .AddQueryParameter("orderByColumn", options.OrderBy)
            .AddQueryParameter("orderDesc", options.OrderDesc);
        
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

    /// <inheritdoc />
    public async Task SendTerminalCommandAsync(TerminalCommandSendingOptions options, CancellationToken cancellationToken)
    {
        ThrowIfNotAuthenticated();

        var request = new RestRequest($"/terminals/{options.TerminalId}/commands")
            .AddQueryParameter("token", accessToken)
            .AddBody(PrepareJsonPayload(options), ContentType.Json);

        await client.PostAsync(request, cancellationToken);
    }

    private string PrepareJsonPayload(TerminalCommandSendingOptions options)
    {
        var payload = new JObject
        {
            ["command_id"] = options.CommandTypeId
        };

        var commandValues = options.CommandValues.ToList();
        for (int i = 0; i < commandValues.Count; i++)
        {
            payload[$"parameter{i + 1}"] = commandValues[i];
        }

        return payload.ToString();
    }

    #region Parse terminal command

    private TerminalCommand ParseTerminalCommand(JToken terminalCommand)
    {
        var command = new TerminalCommand
        {
            Id = terminalCommand.Value<int>("id"),
            TerminalId = terminalCommand.Value<int>("terminal_id"),
            CommandId = terminalCommand.Value<int>("command_id"),
            State = terminalCommand.Value<string>("state_name"),
            CreatedAt = terminalCommand.Value<DateTime>("time_created"),
            Values = ParseValues(terminalCommand)
        };

        return command;
    }

    private IEnumerable<int> ParseValues(JToken terminalCommand)
    {
        var values = new List<int>();
        
        for (int i = 1; i < int.MaxValue; i++)
        {
            var value = terminalCommand.Value<int?>($"parameter{i}");
            if (!value.HasValue)
            {
                break;
            }
                
            values.Add(value.Value);
        }

        return values;
    }

    #endregion

    #region Parse command type
    
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

    #endregion
    
    private void ThrowIfNotAuthenticated()
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new InfrastructureException("The client is not initialized");
        }
    }
    
    /// <inheritdoc />
    public void Dispose()
    {
        client.Dispose();
    }
}