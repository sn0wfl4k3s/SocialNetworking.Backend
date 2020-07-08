using System;

namespace Service.Mediator.V1.ProfileCase.GetProfile
{
    public class GetProfileVM
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Biography { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Birth { get; set; }
    }
}
