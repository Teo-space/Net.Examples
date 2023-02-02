
using Net.Examles.Examples.CQRS.Interfaces;

namespace Net.Examles.Examples.CQRS.Dispatchers;


record CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{

    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();

        return handler.Handle(command, cancellation);
    }

}

