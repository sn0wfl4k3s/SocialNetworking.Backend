using Core.Service.Requests;
using MediatR;

namespace Core.Service.Handlers
{
    public interface ICriarHandler<TRequest, TResponse> : IRequestHandler<CriarRequest<TRequest, TResponse>, Response<TResponse>>
    {
    }
}
