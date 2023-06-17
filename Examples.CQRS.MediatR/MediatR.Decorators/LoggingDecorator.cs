using MediatR;

namespace Examples.CQRS.MediatR.MediatR.Decorators;


class LoggingDecorator<TRequest, TResponse>(
    IRequestHandler<TRequest, TResponse> handler,
        ILogger logger)

    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            logger.Info($"LoggingDecorator.  Before execution for {typeof(TRequest).Name}");

            return handler.Handle(request, cancellationToken);
        }
        finally
        {
            logger.Info($"LoggingDecorator.  After execution for {typeof(TRequest).Name}");
        }
    }
}