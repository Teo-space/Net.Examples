using Examples.CQRS.Dispatcher.Interfaces;

namespace Examples.CQRS.Dispatcher.Dispatchers;

/// <summary>
/// Диспетчер запросов
/// нужен для получения соответствующего хэндлера и сквозной функциональности
/// </summary>
/// <param name="serviceProvider"></param>
class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();

        return handler.Handle(query, cancellation);
    }


}

