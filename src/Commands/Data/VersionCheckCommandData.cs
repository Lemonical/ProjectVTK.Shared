using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandType("CHECK_VERSION")]
public record struct VersionCheckCommandData : ICommandData
{
    public float Version { get; set; }
}
