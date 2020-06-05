using Core.Domain;
using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Post : IEntity
    {
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual User Author { get; set; }
        public virtual IEnumerable<File> Files { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
