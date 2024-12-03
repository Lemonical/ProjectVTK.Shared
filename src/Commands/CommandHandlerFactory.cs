namespace ProjectVTK.Shared.Commands;

public class CommandHandlerFactory(IEnumerable<ICommandHandler> handlers)
{
    private readonly IEnumerable<ICommandHandler> _handlers = handlers;

    public ICommandHandler? GetHandler(CommandProtocols protocol)
    {
        foreach (var handler in _handlers)
            if (handler.CanHandle(protocol))
                return handler;
        return null;
    }
}

