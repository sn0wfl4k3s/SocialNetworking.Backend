using Core.Service.Requests;
using MediatR;

namespace Core.Service.Handlers
{
    public interface IListarHandler<TResponse> : IRequestHandler<ListarRequest<TResponse>, Response<TResponse>>
    {
    }
}
