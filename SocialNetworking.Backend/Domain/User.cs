using Core;

namespace Domain
{
    public class User : IEntity
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
