using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface IRemoverHandler<TSource, TResponse> : IRequestHandler<RemoverRequest<TSource, TResponse>, Response<TResponse>>
    {
    }
}
