using Core.Service.Core;
using Domain.Entity;
using Domain.ViewModels.Friendship;

namespace Service.Mediator.V1.FriendshipCase.AcceptFriend
{
    public class AcceptFriendCommand : IRequestUser<FriendshipResponse>
    {
        public User User { get; set; }
        public string Username { get; set; }

        public AcceptFriendCommand(string username)
        {
            Username = username;
        }
    }
}
