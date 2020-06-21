using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface IAtualizarHandler<TRequest, TResponse> : IRequestHandler<AtualizarRequest<TRequest, TResponse>, Response<TResponse>>
    {
    }
}
