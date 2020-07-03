using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Comment
{
    public class CommentRequest
    {
        public ulong? Id { get; set; }

        [Required]
        public ulong PostId { get; set; }

        [Required]
        public string Description { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
