using Domain.Entity;
using InfraData.Core;

namespace InfraData.Repository
{
    public class FileReferenceRepository : RepositoryCore<FileReference>
    {
        public FileReferenceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
