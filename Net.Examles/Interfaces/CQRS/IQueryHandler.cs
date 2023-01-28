namespace Net.Examles.Interfaces.CQRS;


interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery
{
    public Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}
