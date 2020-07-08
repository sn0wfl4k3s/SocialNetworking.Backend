using Core.Service.Core;
using Domain.Entity;

namespace Core.Service.Requests
{
    public class CriarRequest<TRequest, TResponse> : IUserCommandRequest<TRequest, TResponse>
    {
        public User User { get; set; }
        public TRequest Entidade { get; set; }

        public CriarRequest(TRequest entidade)
        {
            Entidade = entidade;
        }
    }
}
