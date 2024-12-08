using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.PublicizeServer)]
public readonly record struct PublicizeServerCommandData : ICommandData
{
    public string Name { get; init; }
    public int Port { get; init; }
    public ushort UserCount { get; init; }
    public ushort MaxUserCount { get; init; }
}

