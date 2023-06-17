namespace Examples.CQRS.ViaDispatcher.Interfaces;

interface ICommandDispatcher
{
    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation);
}