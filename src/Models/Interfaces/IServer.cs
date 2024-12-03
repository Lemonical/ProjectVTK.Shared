namespace ProjectVTK.Shared.Models.Interfaces;

public interface IServer
{
    string Name { get; }
    string IpAddress { get; }
    int Port { get; }
    ushort UserCount { get; }
    ushort MaxUserCount { get; }
}
