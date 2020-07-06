using Core.Service.Core;
using Domain.Entity;

namespace Core.Service.Requests
{
    public class ListarRequest<TResponse> : IRequestUser<TResponse>
    {
        public User User { get; set; }

        public string Parameter { get; set; }
    }
}
