namespace Net.Examles.Interfaces.CQRS;


interface ICommandDispatcher
{
    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        where TCommand : ICommand
        ;
}
