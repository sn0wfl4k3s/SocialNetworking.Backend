using Core.Service.Requests;
using MediatR;

namespace Core.Service.Handlers
{
    public interface IAtualizarHandler<TRequest, TResponse> : IRequestHandler<AtualizarRequest<TRequest, TResponse>, Response<TResponse>>
    {
    }
}
