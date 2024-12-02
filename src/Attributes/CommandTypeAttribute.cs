namespace ProjectVTK.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class CommandTypeAttribute(string commandType) : Attribute
{
    public string CommandType { get; } = commandType;
}