namespace Net.Examles.Examples.CQRS.Interfaces;



interface IQueryHandler<in TQuery, TQueryResult> 
{
    public Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}

interface IQueryDispatcher
{
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation);
}



interface ICommandHandler<in TCommand, TCommandResult>
{
    public Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}


interface ICommandDispatcher
{
    public Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation);
}

