namespace Net.Examles.Examples.CQRS.Interfaces;


public interface IBaseRequest
{
}
public interface IRequest<out TResponse> : IBaseRequest
{
}

/*
public interface IRequest : IRequest<MediatR.Unit>,
    MediatR.IRequest<MediatR.Unit>
{
}
*/