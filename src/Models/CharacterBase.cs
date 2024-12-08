using ProjectVTK.Shared.Models.Interfaces;

namespace ProjectVTK.Shared.Models;

public abstract class CharacterBase(ushort id, string folderName, bool isUsed) : ICharacter
{
    public ushort Id { get; set; } = (ushort)(id == 0 ? 1 : id);

    public string FolderName { get; set; } = folderName;

    public bool IsUsed { get; set; } = isUsed;
}
