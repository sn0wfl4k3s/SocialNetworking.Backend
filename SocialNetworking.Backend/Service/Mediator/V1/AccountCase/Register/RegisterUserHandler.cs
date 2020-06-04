using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Mediator.V1.AccountCase.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<RegisterUserVM>>
    {

        public Task<Response<RegisterUserVM>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
