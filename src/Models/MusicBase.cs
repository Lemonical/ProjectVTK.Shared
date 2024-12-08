using ProjectVTK.Shared.Models.Interfaces;

namespace ProjectVTK.Shared.Models;

public abstract class MusicBase(ushort id, string name, string fileName) : IMusic
{
    public ushort Id { get; set; } = (ushort)(id == 0 ? 1 : id);

    public string Name { get; set; } = name;

    public string FileName { get; set; } = fileName;
}
