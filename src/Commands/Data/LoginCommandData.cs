using ProjectVTK.Shared.Attributes;

namespace ProjectVTK.Shared.Commands.Data;

[CommandType("LOGIN")]
public record struct LoginCommandData : ICommandData
{
    public string Username { get; set; }
    public string Password { get; set; }
}