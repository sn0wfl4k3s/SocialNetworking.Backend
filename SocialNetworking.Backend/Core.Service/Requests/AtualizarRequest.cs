using Core.Service.Core;
using Domain.Entity;

namespace Core.Service.Requests
{
    public class AtualizarRequest<TRequest, TResponse> : IUserCommandRequest<TRequest, TResponse>
    {
        public User User { get; set; }
        public TRequest Entidade { get; set; }
        public ulong Id { get; set; }

        public AtualizarRequest(TRequest entidade, ulong id)
        {
            Entidade = entidade;
            Id = id;
        }
    }
}
