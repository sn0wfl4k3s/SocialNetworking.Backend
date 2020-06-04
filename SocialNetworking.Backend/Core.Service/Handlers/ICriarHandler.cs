using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface ICriarHandler<TSource, TResponse> : IRequestHandler<CriarRequest<TSource, TResponse>, Response<TResponse>>
    {
    }
}
