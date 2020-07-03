using Core.Domain;
using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Comment : IEntity
    {
        public ulong Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public virtual IEnumerable<FileReference> FileReferences { get; set; }
    }
}