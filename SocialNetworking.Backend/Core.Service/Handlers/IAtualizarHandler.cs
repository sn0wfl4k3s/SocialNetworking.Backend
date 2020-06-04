using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface IAtualizarHandler<TSource, TResponse> : IRequestHandler<AtualizarRequest<TSource, TResponse>, Response<TResponse>>
    {
    }
}
