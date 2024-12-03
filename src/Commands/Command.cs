using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Helpers;
using System.Text.Json;

namespace ProjectVTK.Shared.Commands;

public enum CommandStatusCode
{
    Success,
    Failed
}

public readonly record struct Command
{
    public string CommandType { get; init; }
    public Guid? Id { get; init; }
    public CommandStatusCode? StatusCode { get; init; }
    public string? ErrorMessage { get; init; }
    public object Data { get; init; }

    /// <summary>
    /// Creates a new command request object
    /// </summary>
    /// <param name="data">Command data inheriting ICommandData interface</param>
    /// <returns>string in JSON format</returns>
    public static string CreateRequest(ICommandData data)
        => JsonSerializer.Serialize(CreateInstance(data, new()), JsonHelper.GetSerializerOptions());

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

    private static Command CreateInstance(ICommandData data, Guid? requestGuid = null, CommandStatusCode? statusCode = null, string? errorMessage = null)
    {
        var commandType = (data.GetType()
            .GetCustomAttributes(typeof(CommandTypeAttribute), false)
            .OfType<CommandTypeAttribute>()
            .FirstOrDefault()?.CommandType) ?? throw new InvalidOperationException($"No CommandTypeAttribute defined on {data.GetType().Name}");

        var command = new Command
        {
            CommandType = commandType,
            Id = requestGuid,
            StatusCode = statusCode,
            ErrorMessage = errorMessage,
            Data = data
        };

        return command;
    }
}