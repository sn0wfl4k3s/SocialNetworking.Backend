﻿using Core.Service;
using MediatR;

namespace Service.Mediator.V1.AccountCase.Login
{
    public class LoginUserCommand : IRequest<Response<LoginUserVM>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
