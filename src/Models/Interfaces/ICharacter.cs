namespace ProjectVTK.Shared.Models.Interfaces;

public interface ICharacter
{
    ushort Id { get; }
    string FolderName { get; }
    bool IsUsed { get; }
}
