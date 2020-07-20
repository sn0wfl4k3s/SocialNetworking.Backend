using Domain.ViewModels.User;
using System;

namespace Domain.ViewModels.Friendship
{
    public class FriendshipResponse
    {
        public ulong Id { get; set; }
        public UserResponse From { get; set; }
        public UserResponse To { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
    }
}