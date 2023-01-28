namespace Net.Examles.Interfaces.CQRS;


interface ICommandHandler<in TCommand, TCommandResult> where TCommand : ICommand
{
    public Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}
