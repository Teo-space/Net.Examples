namespace Examples.CQRS.ViaDispatcher.Interfaces;

interface IQueryHandler<in TQuery, TQueryResult>
{
    public Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}

