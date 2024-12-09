using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Models.Interfaces;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.GetServerMetadata)]
public readonly record struct GetServerMetadataCommandData : ICommandData
{
    public TimeSpan Offset { get; init; }
    public IEnumerable<ICharacter> Characters { get; init; }
    public IEnumerable<IMusic> Music { get; init; }
    public IEnumerable<IArea> Areas { get; init; }
}
