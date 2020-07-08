using Core.Service.Core;
using Domain.Entity;

namespace Core.Service.Requests
{
    public class ProcurarRequest<TRequest, TResponse> : IUserCommandRequest<TRequest, TResponse>
    {
        public User User { get; set; }
        public TRequest Entidade { get; set; }

        public ProcurarRequest(TRequest entidade)
        {
            Entidade = entidade;
        }
    }
}
