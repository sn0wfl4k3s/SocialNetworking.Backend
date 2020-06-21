using Core.Service.Requests.Core;
using Domain.Entity;

namespace Core.Service.Requests
{
    public class AtualizarRequest<TRequest, TResponse> : IRequestUser<TResponse>
    {
        public User User { get; set; }
        public TRequest Entidade { get; set; }
        public ulong Id { get; set; }
    }
}
