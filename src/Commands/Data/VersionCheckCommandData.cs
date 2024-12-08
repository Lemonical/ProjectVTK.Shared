using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.VersionCheck)]
public readonly record struct VersionCheckCommandData : ICommandData
{
    public ClientType? Type { get; init; }
    public float? Version { get; init; }
    public string? Message { get; init; }
}

public enum ClientType
{
    Client,
    Server
}
