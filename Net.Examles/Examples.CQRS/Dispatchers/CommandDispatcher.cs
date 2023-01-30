using Net.Examles.Examples.CQRS.Interfaces;

namespace Net.Examles.Examples.CQRS.Dispatchers;


record CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{

    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>
        (TCommand command, CancellationToken cancellation) 
        where TCommand : ICommand<TCommandResult>
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();

        return handler.Handle(command, cancellation);
    }

    /*
    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }
    */
}
