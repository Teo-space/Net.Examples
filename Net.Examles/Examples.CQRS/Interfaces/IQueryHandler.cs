namespace Net.Examles.Examples.CQRS.Interfaces;


interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
{
    public Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}
