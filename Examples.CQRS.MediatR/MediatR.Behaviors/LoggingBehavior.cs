using MediatR;


namespace Examples.CQRS.MediatR.MediatR.Behaviors;


class LoggingBehavior<TRequest, TResponse>(ILogger logger)

    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            logger.Info($"LoggingBehavior.  Before execution for {typeof(TRequest).Name}");

            return await next();
        }
        finally
        {
            logger.Info($"LoggingBehavior.  After execution for {typeof(TRequest).Name}");
        }
    }
}