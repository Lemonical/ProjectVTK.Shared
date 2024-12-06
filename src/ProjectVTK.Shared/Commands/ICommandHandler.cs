﻿using Fleck;

namespace ProjectVTK.Shared.Commands;

public interface ICommandHandler
{
    bool CanHandle(CommandProtocols protocol);
    Task HandleAsync(Command command, IWebSocketConnection socket);
}
