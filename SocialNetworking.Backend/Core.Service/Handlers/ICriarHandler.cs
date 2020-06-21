using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface ICriarHandler<TRequest, TResponse> : IRequestHandler<CriarRequest<TRequest, TResponse>, Response<TResponse>>
    {
    }
}
