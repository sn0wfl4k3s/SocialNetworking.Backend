using Core.Domain;
using Domain.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrossCutting.Account
{
    public class AccountService : IAccountService
    {
        private readonly IEntityRepository<User> _repository;

        public AccountService(IEntityRepository<User> repository)
        {
            _repository = repository;
        }

        public string GenerateUsername(string name, string lastname)
        {
            string username = "{0}.{1}.{2}";

            do
            {
                int random = new Random().Next();

                username = string.Format(username, name.ToLower(), lastname.ToLower(), random);

            } while (_repository.ObterQueryEntidade().Any(u => u.Username == username));

            return username;
        }

        public async Task<string> GenerateUsernameAsync(string name, string lastname)
        {
            var username = GenerateUsername(name, lastname);

            return await Task.FromResult(username);
        }
    }
}
