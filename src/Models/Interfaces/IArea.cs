namespace ProjectVTK.Shared.Models.Interfaces;

public interface IArea
{
    ushort Id { get; }
    string Name { get; }
    string FileName { get; }
    ushort UserCount { get; }
}
