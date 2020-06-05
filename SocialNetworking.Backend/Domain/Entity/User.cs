using Core.Domain;
using System;

namespace Domain.Entity
{
    public class User : IEntity
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Birth { get; set; }
        public virtual File ProfilePicture { get; set; }
    }
}
