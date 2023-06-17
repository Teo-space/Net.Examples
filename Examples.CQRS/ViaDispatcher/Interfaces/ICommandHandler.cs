namespace Examples.CQRS.ViaDispatcher.Interfaces;

interface ICommandHandler<in TCommand, TCommandResult>
{
    public Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}
