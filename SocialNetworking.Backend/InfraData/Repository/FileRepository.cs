using Domain.Entity;
using InfraData.Core;

namespace InfraData.Repository
{
    public class FileRepository : RepositoryCore<File>
    {
        public FileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
