namespace Net.Examles.Interfaces.CQRS;


public interface IBaseRequest :
    MediatR.IBaseRequest
{
}
public interface IRequest<out TResponse> : IBaseRequest,
    MediatR.IBaseRequest
{
}

public interface IRequest : IRequest<MediatR.Unit>,
    MediatR.IRequest<MediatR.Unit>
{
}