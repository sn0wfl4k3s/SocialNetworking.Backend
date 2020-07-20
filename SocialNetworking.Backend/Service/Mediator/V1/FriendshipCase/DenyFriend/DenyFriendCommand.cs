using Core.Service.Core;
using Domain.Entity;
using Domain.ViewModels.Friendship;

namespace Service.Mediator.V1.FriendshipCase.DenyFriend
{
    public class DenyFriendCommand : IRequestUser<FriendshipResponse>
    {
        public User User { get; set; }
        public string Username { get; set; }

        public DenyFriendCommand(string username)
        {
            Username = username;
        }
    }
}
