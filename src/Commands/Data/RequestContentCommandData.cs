using ProjectVTK.Shared.Attributes;
using ProjectVTK.Shared.Models.Interfaces;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.RequestContent)]
public readonly record struct RequestContentCommandData : ICommandData
{
    public IEnumerable<ICharacter> Characters { get; init; }
    public IEnumerable<IMusic> Music { get; init; }
    public IEnumerable<IArea> Areas { get; init; }
}
