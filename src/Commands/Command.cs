using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Commands.Data;
using ProjectVTK.Shared.Helpers;
using System.Text.Json;

namespace ProjectVTK.Shared.Commands;

public record struct Command
{
    public string CommandType { get; set; }
    public object Data { get; set; }

    public static string Create(ICommandData data)
    {
        var commandType = (data.GetType()
            .GetCustomAttributes(typeof(CommandTypeAttribute), false)
            .OfType<CommandTypeAttribute>()
            .FirstOrDefault()?.CommandType) ?? throw new InvalidOperationException($"No CommandTypeAttribute defined on {data.GetType().Name}");

        var command = new Command
        {
            CommandType = commandType,
            Data = data
        };

        return JsonSerializer.Serialize(command, JsonHelper.GetSerializerOptions());
    }
}