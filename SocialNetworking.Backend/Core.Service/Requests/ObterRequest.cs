using MediatR;

namespace Core.Service.Requests
{
    public class ObterRequest<TResponse> : IRequest<Response<TResponse>>
    {
        public ulong Id { get; set; }
    }
}
