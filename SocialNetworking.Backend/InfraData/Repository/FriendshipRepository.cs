using Domain.Entity;
using InfraData.Core;

namespace InfraData.Repository
{
    public class FriendshipRepository : RepositoryCore<Friendship>
    {
        public FriendshipRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
