using Core.Service;
using MediatR;

namespace Service.Mediator.V1.AccountCase.Register
{
    public class RegisterUserCommand : IRequest<Response<RegisterUserVM>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
