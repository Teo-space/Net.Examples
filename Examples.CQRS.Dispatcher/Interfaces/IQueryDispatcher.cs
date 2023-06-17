namespace Examples.CQRS.Dispatcher.Interfaces;

interface IQueryDispatcher
{
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation);
}
