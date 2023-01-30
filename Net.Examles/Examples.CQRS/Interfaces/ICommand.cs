namespace Net.Examles.Examples.CQRS.Interfaces;



public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
