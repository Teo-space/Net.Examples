namespace Net.Examles.Interfaces.CQRS;


public interface IQuery :
    IRequest
{
}
public interface IQuery<out TResponse> :
    IRequest<TResponse>
{
}
