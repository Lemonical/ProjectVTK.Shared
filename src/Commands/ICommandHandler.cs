using Fleck;

namespace ProjectVTK.Shared.Commands;

public interface ICommandHandler
{
    bool CanHandle(string commandType);
    Task HandleAsync(Command command, IWebSocketConnection socket);
}

