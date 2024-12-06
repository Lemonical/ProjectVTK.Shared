using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.Login)]
public readonly record struct LoginCommandData : ICommandData
{
    public string? Username { get; init; }
    public string? Password { get; init; }
    public Guid? SessionId { get; init; }
}