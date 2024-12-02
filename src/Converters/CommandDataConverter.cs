using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Commands.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectVTK.Shared.Converters;

public class CommandDataConverter : JsonConverter<ICommandData>
{
    public override ICommandData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        if (root.TryGetProperty("command_type", out var commandTypeProperty))
        {
            var commandType = commandTypeProperty.GetString();

            var dataType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(CommandTypeAttribute), false).Length != 0)
                .FirstOrDefault(t =>
                    t.GetCustomAttributes(typeof(CommandTypeAttribute), false)
                    .OfType<CommandTypeAttribute>()
                    .Any(attr => attr.CommandType == commandType)) ?? throw new InvalidOperationException($"Unknown command type: {commandType}");

            var dataJson = root.GetProperty("data").GetRawText();
            return (ICommandData?)JsonSerializer.Deserialize(dataJson, dataType, options);
        }

        throw new JsonException("Invalid command JSON format");
    }

    public override void Write(Utf8JsonWriter writer, ICommandData value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}

