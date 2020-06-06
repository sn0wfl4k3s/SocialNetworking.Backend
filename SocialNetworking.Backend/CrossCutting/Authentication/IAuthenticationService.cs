using Domain.Entity;
using System;

namespace CrossCutting.Authentication
{
    public interface IAuthenticationService
    {
        (string Token, DateTime Expires, string Type) CreateTokenJWT(User user);
    }
}
