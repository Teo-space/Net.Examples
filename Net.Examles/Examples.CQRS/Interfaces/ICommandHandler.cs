namespace Net.Examles.Examples.CQRS.Interfaces;



interface ICommandHandler<in TCommand, TCommandResult> where TCommand : ICommand<TCommandResult>
{
    public Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}
