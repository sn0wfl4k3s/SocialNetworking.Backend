using Domain.Entity;
using InfraData.Core;

namespace InfraData.Repository
{
    public class PostRepository : RepositoryCore<Post>
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
