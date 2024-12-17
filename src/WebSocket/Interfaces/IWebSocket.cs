using ProjectVTK.Shared.Commands;

namespace ProjectVTK.Shared.WebSocket.Interfaces;

public interface IWebSocket
{
    Task Send(Command command);
    void Close();
}
