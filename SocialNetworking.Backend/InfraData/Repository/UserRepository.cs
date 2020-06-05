using Domain.Entity;
using InfraData.Core;

namespace InfraData.Repository
{
    public class UserRepository : RepositoryCore<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
