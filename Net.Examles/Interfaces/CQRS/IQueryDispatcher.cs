namespace Net.Examles.Interfaces.CQRS;


interface IQueryDispatcher
{
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        where TQuery : IQuery
        ;
}