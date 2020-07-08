using Core.Service.Requests;
using MediatR;

namespace Core.Service.Handlers
{
    public interface IProcurarHandler<TRequest, TResponse> : IRequestHandler<ProcurarRequest<TRequest, TResponse>, Response<TResponse>>
    {
    }
}
