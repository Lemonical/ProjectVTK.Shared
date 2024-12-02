namespace ProjectVTK.Shared.Commands.Data;

public record struct ErrorResponse : ICommandData
{
    public ErrorCodes ErrorCode { get; set; }
    public string Message { get; set; }
}

public enum ErrorCodes
{
    Invalid = 0,
    InvalidArgs = 1,
}

