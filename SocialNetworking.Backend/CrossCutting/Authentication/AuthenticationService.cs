using Core.Domain;
using CrossCutting.Configuration;
using CrossCutting.Constants;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _settings;
        private readonly IEntityRepository<User> _repository;

        public AuthenticationService(IOptions<JwtSettings> options, IEntityRepository<User> repository)
        {
            _settings = options.Value;
            _repository = repository;
        }

        public (string Token, DateTime Expires, string Type) CreateTokenJWT(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.SigningKey);

            var securitykey = new SymmetricSecurityKey(key);

            var expires = DateTime.UtcNow.AddMinutes(_settings.ValidTokenMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, Roles.User)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = handler.CreateToken(tokenDescriptor);

            var token = handler.WriteToken(securityToken);

            return (token, expires, "Bearer");
        }

        public async Task<(string Token, DateTime Expires, string Type)> CreateTokenJWTAsync(User user)
        {
            return await Task.FromResult(CreateTokenJWT(user));
        }

        public User GetUserByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token);

            var username = jwt.Claims.First(c => c.Type == "unique_name").Value;

            var user = _repository.ObterQueryEntidade().AsNoTracking().FirstOrDefault(u => u.Username == username);

            return user;
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await Task.FromResult(GetUserByToken(token));
        }
    }
}
