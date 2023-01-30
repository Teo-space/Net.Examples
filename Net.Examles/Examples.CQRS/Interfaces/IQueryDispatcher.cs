namespace Net.Examles.Examples.CQRS.Interfaces;


interface IQueryDispatcher
{
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation) 
        where TQuery : class, IQuery<TQueryResult>
        ;
}

