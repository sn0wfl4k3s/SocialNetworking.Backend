using MediatR;

namespace Core.Service.Core
{
    public interface IRequestUserHandler<TRequest, TResponse> : IRequestHandler<TRequest, Response<TResponse>>
        where TRequest : IRequestUser<TResponse>
    {
    }
}
