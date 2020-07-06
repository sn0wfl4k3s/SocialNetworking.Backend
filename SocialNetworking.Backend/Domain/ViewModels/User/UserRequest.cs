using System;

namespace Domain.ViewModels.User
{
    public class UserRequest
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
        public DateTime? Birth { get; set; }
    }
}
