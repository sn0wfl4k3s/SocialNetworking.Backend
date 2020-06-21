using Core.Service.Requests.Core;
using Domain.Entity;

namespace Core.Service.Requests
{
    public class CriarRequest<TRequest, TResponse> : IRequestUser<TResponse>
    {
        public User User { get; set; }
        public TRequest Entidade { get; set; }
    }
}
