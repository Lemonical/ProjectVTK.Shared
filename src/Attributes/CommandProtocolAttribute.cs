using ProjectVTK.Shared.Commands;

namespace ProjectVTK.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class CommandProtocolAttribute(CommandProtocols protocol) : Attribute
{
    public CommandProtocols Protocol { get; } = protocol;
}