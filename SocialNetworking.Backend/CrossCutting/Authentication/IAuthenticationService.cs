using Domain.Entity;
using System;
using System.Threading.Tasks;

namespace CrossCutting.Authentication
{
    public interface IAuthenticationService
    {
        Task<(string Token, DateTime Expires, string Type)> CreateTokenJWTAsync(User user);
        (string Token, DateTime Expires, string Type) CreateTokenJWT(User user);
        Task<User> GetUserByTokenAsync(string token);
        User GetUserByToken(string token);
    }
}
