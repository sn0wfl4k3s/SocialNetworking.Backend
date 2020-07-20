using Core.Service.Core;
using Domain.Entity;
using Domain.ViewModels.Friendship;

namespace Service.Mediator.V1.FriendshipCase.RequestFor
{
    public class RequestForCommand : IRequestUser<FriendshipResponse>
    {
        public User User { get; set; }

        public string Username { get; set; }

        public RequestForCommand(string username)
        {
            Username = username;
        }
    }
}
