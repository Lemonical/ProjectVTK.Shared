using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.VersionCheck)]
public readonly record struct VersionCheckCommandData : ICommandData
{
    public float? Version { get; init; }
    public string? Message { get; init; }
}
