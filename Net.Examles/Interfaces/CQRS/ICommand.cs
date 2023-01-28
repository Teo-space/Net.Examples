namespace Net.Examles.Interfaces.CQRS;


public interface ICommand : IRequest
{
}


public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
