using Domain.Entity;
using MediatR;

namespace Core.Service.Core
{
    public interface IRequestUser<TResponse> : IRequest<Response<TResponse>>
    {
        User User { get; set; }
    }
}
