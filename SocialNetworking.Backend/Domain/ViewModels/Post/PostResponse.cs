using Domain.Entity;
using Domain.ViewModels.Comment;
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
        public virtual IEnumerable<FileReference> FileReferences { get; set; }
        public virtual IEnumerable<CommentResponse> Comments { get; set; }
    }
}
