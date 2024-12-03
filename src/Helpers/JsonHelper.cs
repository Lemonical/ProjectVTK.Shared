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
            WriteIndented = isIdented
        };

        options.Converters.Add(new CommandDataConverter());
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));

        return options;
    }
}
