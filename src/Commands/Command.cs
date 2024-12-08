using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Helpers;
using System.Text.Json;

namespace ProjectVTK.Shared.Commands;

public enum CommandStatusCode { Success, Failed }

public enum CommandProtocols
{
    Unknown,
    Login,
    CreateAccount,
    VersionCheck,
    GetServers,
    UpdateServer,
    RequestDownload,
}

public readonly record struct Command
{
    public CommandProtocols Protocol { get; init; }
    public Guid? Id { get; init; }
    public CommandStatusCode? Status { get; init; }
    public string? ErrorMessage { get; init; }
    public object? Data { get; init; }

    /// <summary>
    /// Creates a new command request object
    /// </summary>
    /// <param name="data">Command data inheriting ICommandData interface</param>
    /// <returns>string in JSON format</returns>
    public static string CreateRequest(ICommandData data)
        => JsonSerializer.Serialize(CreateInstance(data, Guid.NewGuid()), JsonHelper.GetSerializerOptions());

    /// <summary>
    /// Creates a new command request object
    /// </summary>
    /// <param name="protocol">The command protocol</param>
    /// <returns>string in JSON format</returns>
    public static string CreateRequest(CommandProtocols protocol)
        => JsonSerializer.Serialize(CreateInstance(protocol, Guid.NewGuid()), JsonHelper.GetSerializerOptions());

    /// <summary>
    /// Creates a new command request object
    /// </summary>
    /// <param name="requestGuid">GUID from the command request to respond to</param>
    /// <param name="data">Command data inheriting ICommandData interface</param>
    /// <param name="statusCode">Success or Failed status response code</param>
    /// <param name="errorMessage">Message detailing the error or failure in handling the command, if any</param>
    /// <returns>string in JSON format</returns>
    public static string CreateResponse(Guid requestGuid, ICommandData data, CommandStatusCode statusCode, string? errorMessage = null)
        => JsonSerializer.Serialize(CreateInstance(data, requestGuid, statusCode, errorMessage), JsonHelper.GetSerializerOptions());

    /// <summary>
    /// Creates a new command request object
    /// </summary>
    /// <param name="requestGuid">GUID from the command request to respond to</param>
    /// <param name="protocol">The type of command</param>
    /// <param name="statusCode">Success or Failed status response code</param>
    /// <param name="errorMessage">Message detailing the error or failure in handling the command, if any</param>
    /// <returns>string in JSON format</returns>
    public static string CreateResponse(Guid requestGuid, CommandProtocols protocol, CommandStatusCode statusCode, string? errorMessage = null)
        => JsonSerializer.Serialize(CreateInstance(protocol, requestGuid, statusCode, errorMessage), JsonHelper.GetSerializerOptions());

    private static Command CreateInstance(CommandProtocols protocol, Guid? requestGuid = null, CommandStatusCode? statusCode = null, string? errorMessage = null)
    {
        var command = new Command
        {
            Protocol = protocol,
            Id = requestGuid,
            Status = statusCode,
            ErrorMessage = errorMessage
        };

        return command;
    }

    private static Command CreateInstance(ICommandData data, Guid? requestGuid = null, CommandStatusCode? statusCode = null, string? errorMessage = null)
    {
        var protocol = (data.GetType()
            .GetCustomAttributes(typeof(CommandProtocolAttribute), false)
            .OfType<CommandProtocolAttribute>()
            .FirstOrDefault()?.Protocol) ?? throw new InvalidOperationException($"No ProtocolAttribute defined on {data.GetType().Name}");

        var command = new Command
        {
            Protocol = protocol,
            Id = requestGuid,
            Status = statusCode,
            ErrorMessage = errorMessage,
            Data = data
        };

        return command;
    }
}