using Net.Examles.Examples.CQRS.Interfaces;

namespace Net.Examles.Examples.CQRS.Dispatchers;


record QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();

        return handler.Handle(query, cancellation);
    }


}

