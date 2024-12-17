namespace ProjectVTK.Shared.WebSocket.Interfaces;

public interface IWebSocketSession : IWebSocket
{
    Guid Id { get; }
    string Username { get; }
    string IpAddress { get; }
}