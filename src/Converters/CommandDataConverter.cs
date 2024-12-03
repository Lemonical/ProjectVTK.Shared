using ProjectVTK.Shared.Commands;
using ProjectVTK.Shared.Commands.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectVTK.Shared.Converters;

public class CommandDataConverter : JsonConverter<object>
{
    private readonly Dictionary<CommandProtocols, Type> _dataTypeMappings;

    public CommandDataConverter()
    {
        // TODO: Need more
        _dataTypeMappings = new Dictionary<CommandProtocols, Type>
        {
            { CommandProtocols.Login, typeof(LoginCommandData) },
            { CommandProtocols.VersionCheck, typeof(VersionCheckCommandData) },
            { CommandProtocols.GetServers, typeof(GetServersCommandData) }
        };
    }

    public override bool CanConvert(Type type) 
        => type == typeof(object);

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        if (!root.TryGetProperty("protocol", out var commandProtocolElement))
            throw new JsonException("Missing 'protocol' property in JSON string.");

        if (!Enum.TryParse<CommandProtocols>(commandProtocolElement.GetString(), true, out var commandType))
            throw new JsonException($"Unknown protocol: {commandProtocolElement.GetString()}");

        if (!root.TryGetProperty("data", out var dataElement))
            throw new JsonException("Missing 'data' property in JSON string.");

        if (!_dataTypeMappings.TryGetValue(commandType, out var dataType))
            throw new JsonException($"No data type mapping found for command type: {commandType}");

        return JsonSerializer.Deserialize(dataElement.GetRawText(), dataType, options);
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        throw new NotSupportedException("Writing CommandData directly is not supported.");
    }
}


