using Core.Service.Core;
using Domain.Entity;
using Domain.ViewModels.User;
using System.Collections.Generic;

namespace Service.Mediator.V1.FriendshipCase.ListAllRequest
{
    public class ListAllRequestQuery : IRequestUser<IEnumerable<UserResponse>>
    {
        public User User { get; set; }
        public string Username { get; set; }

        public ListAllRequestQuery(string username)
        {
            Username = username;
        }
    }
}
