using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.Post
{
    public class PostResponse
    {
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<FileReference> Files { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
