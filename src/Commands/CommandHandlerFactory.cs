namespace ProjectVTK.Shared.Commands;

public class CommandHandlerFactory(IEnumerable<ICommandHandler> handlers)
{
    private readonly IEnumerable<ICommandHandler> _handlers = handlers;

    public ICommandHandler? GetHandler(string commandType)
    {
        foreach (var handler in _handlers)
        {
            if (handler.CanHandle(commandType))
                return handler;
        }
        return null;
    }
}

