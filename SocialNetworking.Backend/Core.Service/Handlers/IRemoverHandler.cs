using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface IRemoverHandler<TRequest, TResponse> : IRequestHandler<RemoverRequest<TRequest, TResponse>, Response<TResponse>>
    {
    }
}
