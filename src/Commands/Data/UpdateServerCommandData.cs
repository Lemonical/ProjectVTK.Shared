using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.UpdateServer)]
public readonly record struct UpdateServerCommandData : ICommandData
{
    public string Name { get; init; }
    public ushort UserCount { get; init; }
    public ushort MaxUserCount { get; init; }
    public bool? ListServer { get; init; }
}
