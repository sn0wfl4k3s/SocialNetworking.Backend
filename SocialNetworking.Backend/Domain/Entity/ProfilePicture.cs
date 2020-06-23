using Core.Domain;

namespace Domain.Entity
{
    public class ProfilePicture : IEntity
    {
        public ulong Id { get; set; }
        public virtual User User { get; set; }
        public virtual FileReference File { get; set; }
    }
}
