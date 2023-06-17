using Examples.CQRS.Dispatcher.Interfaces;

namespace Examples.CQRS.Dispatcher.Dispatchers;


/// <summary>
/// Диспетчер комманд
/// нужен для получения соответствующего хэндлера и сквозной функциональности
/// </summary>
/// <param name="serviceProvider"></param>
class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{

    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();

        return handler.Handle(command, cancellation);
    }

}

