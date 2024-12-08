using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandProtocol(CommandProtocols.CreateAccount)]
public readonly record struct CreateAccountCommandData : ICommandData
{
    public string Username { get; init; }
    public string Password { get; init; }
    public ulong? DiscordId { get; init; }
}