using Core.Service.Core;
using Domain.Entity;
using Domain.ViewModels.User;
using System.Collections.Generic;

namespace Service.Mediator.V1.FriendshipCase.ListAllAccepted
{
    public class ListAllAcceptedQuery : IRequestUser<IEnumerable<UserResponse>>
    {
        public User User { get; set; }
        public string Username { get; set; }

        public ListAllAcceptedQuery(string username)
        {
            Username = username;
        }
    }
}
