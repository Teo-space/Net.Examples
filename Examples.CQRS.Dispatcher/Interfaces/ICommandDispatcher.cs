namespace Examples.CQRS.Dispatcher.Interfaces;

interface ICommandDispatcher
{
    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation);
}