using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface IObterHandler<TResponse> : IRequestHandler<ObterRequest<TResponse>, Response<TResponse>>
    {
    }
}
