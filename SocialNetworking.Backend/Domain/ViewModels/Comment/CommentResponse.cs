using Domain.ViewModels.FileReference;
using System;
using System.Collections.Generic;

namespace Domain.ViewModels.Comment
{
    public class CommentResponse
    {
        public ulong Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public virtual IEnumerable<FileReferenceResponse> FileReferences { get; set; }
    }
}
