using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Models.Interfaces;
using System.Collections.Immutable;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.GetServers)]
public readonly record struct GetServersCommandData : ICommandData
{
    public ImmutableHashSet<IServer>? Servers { get; init; }
}
