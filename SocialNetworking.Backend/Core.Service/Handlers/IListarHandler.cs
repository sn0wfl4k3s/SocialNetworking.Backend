using MediatR;
using Core.Service.Requests;

namespace Core.Service.Handlers
{
    public interface IListarHandler<TResponse> : IRequestHandler<ListarRequest<TResponse>, Response<TResponse>>
    {
    }
}
