using ProjectVTK.Shared.Commands;
using ProjectVTK.Shared.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectVTK.Shared.Helpers;

public static class JsonHelper
{
    public static JsonSerializerOptions GetSerializerOptions(bool isIdented = false)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault,
            WriteIndented = isIdented
        };

        options.Converters.Add(new CommandConverter());
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));

        return options;
    }

    public static string AsJson(this Command command)
        => JsonSerializer.Serialize(command, GetSerializerOptions());
}
