using ProjectVTK.Shared.Models.Interfaces;

namespace ProjectVTK.Shared.Models;

public abstract class AreaBase(ushort id, string name, string fileName, ushort userCount) : IArea
{
    public ushort Id { get; set; } = (ushort)(id == 0 ? 1 : id);

    public string Name { get; set; } = name;

    public string FileName { get; set; } = fileName;

    public ushort UserCount { get; set; } = userCount;
}
