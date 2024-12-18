using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Commands;
using ProjectVTK.Shared.Helpers;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectVTK.Shared.Converters;

public class CommandConverter : JsonConverter<Command>
{
    private readonly Dictionary<CommandProtocols, Type> _mappings;

    public CommandConverter()
    {
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ICommandData).IsAssignableFrom(t) && t.IsDefined(typeof(CommandProtocolAttribute), false));

        var mappings = new Dictionary<CommandProtocols, Type>();
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<CommandProtocolAttribute>()!;
            mappings[attribute.Protocol] = type;
        }

        _mappings = mappings;
    }

    public override Command Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        Guid? id = null;
        CommandProtocols protocol = default;
        CommandStatusCode? statusCode = null;
        string? errorMessage = null;
        ICommandData? data = null;
        var dOptions = new JsonSerializerOptions(JsonHelper.GetSerializerOptions());

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException("Expected PropertyName token");

            string propertyName = reader.GetString()!;
            reader.Read();

            switch (propertyName)
            {
                case "protocol":
                    protocol = JsonSerializer.Deserialize<CommandProtocols>(ref reader, dOptions);
                    break;
                case "id":
                    id = JsonSerializer.Deserialize<Guid?>(ref reader, dOptions);
                    break;
                case "status":
                    statusCode = JsonSerializer.Deserialize<CommandStatusCode?>(ref reader, dOptions);
                    break;
                case "error_message":
                    errorMessage = reader.TokenType == JsonTokenType.Null
                        ? null
                        : JsonSerializer.Deserialize<string>(ref reader, dOptions);
                    break;
                case "data":
                    if (_mappings.TryGetValue(protocol, out var dataType))
                        data = (ICommandData?)JsonSerializer.Deserialize(ref reader, dataType, options);
                    else // Skip the data if no matching protocol type is found
                        reader.Skip();
                    break;
                default: // Skip unknown properties
                    reader.Skip();
                    break;
            }
        }

        return new Command
        {
            Protocol = protocol,
            Id = id,
            Status = statusCode,
            ErrorMessage = errorMessage,
            Data = data
        };
    }

    public override void Write(Utf8JsonWriter writer, Command value, JsonSerializerOptions options)
    {
        var modifiedOptions = new JsonSerializerOptions(options);
        modifiedOptions.Converters.Remove(this);

        JsonSerializer.Serialize(writer, (object)value, modifiedOptions);
    }
}