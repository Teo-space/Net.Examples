namespace Net.Examles.Examples.CQRS.Interfaces;


interface ICommandDispatcher
{
    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        where TCommand : ICommand<TCommandResult>;

    //public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellation);

}
