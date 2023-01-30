namespace Net.Examles.Examples.CQRS.Interfaces;


public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
