using Core.Service.Requests;
using MediatR;

namespace Core.Service.Handlers
{
    public interface IRemoverHandler<TRequest, TResponse> : IRequestHandler<RemoverRequest<TRequest, TResponse>, Response<TResponse>>
    {
    }
}
