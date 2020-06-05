using Domain.Entity;
using InfraData.Core;

namespace InfraData.Repository
{
    public class CommentRepository : RepositoryCore<Comment>
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
