using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.Comment
{
    public class CommentResponse
    {
        public ulong Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<FileReference> FileReferences { get; set; }
    }
}
