using Core.Domain;
using CrossCutting.Configuration;
using Domain.Entity;
using Microsoft.Extensions.Options;

namespace CrossCutting.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _settings;
        private readonly IEntityRepository<User> _repositorio;

        public AuthenticationService(IOptions<JwtSettings> options, IEntityRepository<User> repositorio)
        {
            _settings = options.Value;
            _repositorio = repositorio;
        }

    }
}
