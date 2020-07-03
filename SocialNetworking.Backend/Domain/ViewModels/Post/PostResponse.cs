using Domain.ViewModels.Comment;
using Domain.ViewModels.FileReference;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.Post
{
    public class PostResponse
    {
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string Username { get; set; }
        public IEnumerable<FileReferenceResponse> Files { get; set; }
        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
