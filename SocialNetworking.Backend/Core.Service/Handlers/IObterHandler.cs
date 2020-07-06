using Core.Service.Requests;
using MediatR;

namespace Core.Service.Handlers
{
    public interface IObterHandler<TResponse> : IRequestHandler<ObterRequest<TResponse>, Response<TResponse>>
    {
    }
}
