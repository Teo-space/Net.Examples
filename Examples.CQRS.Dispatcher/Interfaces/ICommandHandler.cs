namespace Examples.CQRS.Dispatcher.Interfaces;

interface ICommandHandler<in TCommand, TCommandResult>
{
    public Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}
