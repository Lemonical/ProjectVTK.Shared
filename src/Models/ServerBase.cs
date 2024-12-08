using ProjectVTK.Shared.Models.Interfaces;

namespace ProjectVTK.Shared.Models;

public abstract class ServerBase(string name, string ipAddress, int port, ushort userCount, ushort maxUserCount) : IServer
{
    public string Name { get; set; } = name;

    public string IpAddress { get; set; } = ipAddress;

    public int Port { get; set; } = port;

    public ushort UserCount { get; set; } = userCount;

    public ushort MaxUserCount { get; set; } = maxUserCount;
}
