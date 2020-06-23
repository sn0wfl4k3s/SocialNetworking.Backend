using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Domain.ViewModels.Post
{
    public class PostRequest
    {
        public ulong Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
