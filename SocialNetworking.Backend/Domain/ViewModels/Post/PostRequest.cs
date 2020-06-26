using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels.Post
{
    public class PostRequest
    {
        public ulong? Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public List<IFormFile> Files { get; set; }
    }
}
