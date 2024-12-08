using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.AuthUser)]
public readonly record struct AuthUserCommandData : ICommandData
{
    public string IpAddress { get; init; }
    public Guid SessionId { get; init; }
    public string? Username { get; init; }
}
